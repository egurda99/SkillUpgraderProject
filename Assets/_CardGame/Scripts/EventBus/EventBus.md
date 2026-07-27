# EventBus — гайд по использованию

## 1. Обзор

`EventBus` — собственная (не Zenject SignalBus, не UniRx) реализация паттерна pub/sub, диспетчеризующая события по их точному C#-типу. Внутри — `Dictionary<Type, List<object>>`, без внешних зависимостей.

Файлы:
- `Assets/_CardGame/Scripts/EventBus/IEventBus.cs` — интерфейс
- `Assets/_CardGame/Scripts/EventBus/EventBus.cs` — реализация
- `Assets/_CardGame/Scripts/EventBus/Subscriber.cs` — обёртка над обработчиком с приоритетом

```csharp
public interface IEventBus
{
    void Subscribe<T>(Action<T> handler, int priority = 0);
    void Unsubscribe<T>(Action<T> handler);
    void RaiseEvent<T>(T evt);
}
```

## 2. API

| Метод | Назначение |
|---|---|
| `Subscribe<T>(Action<T> handler, int priority = 0)` | Подписаться на события типа `T`. Чем выше `priority`, тем раньше вызовется обработчик. |
| `Unsubscribe<T>(Action<T> handler)` | Отписаться от событий типа `T`. |
| `RaiseEvent<T>(T evt)` | Разослать событие всем подписчикам типа `T`. |

Особенности реализации, которые нужно знать:

- **Диспетчеризация строго по точному типу `T`.** Подписка на базовый класс/интерфейс не получит события производного типа.
- **Приоритет пересчитывается при каждом `Subscribe`** — список подписчиков полностью пересортировывается (`List.Sort`, нестабильная сортировка). Порядок вызова обработчиков с одинаковым `priority` не гарантирован.
- **`handler == null` игнорируется** в `Subscribe`/`Unsubscribe` — молча ничего не делает вместо падения с NRE.
- **`RaiseEvent` устойчив к исключениям и реентерабельным вызовам**: перед рассылкой берётся снимок списка подписчиков (`ToArray()`), а вызов каждого обработчика обёрнут в try/catch с `Debug.LogException` — падение одного подписчика или `Subscribe`/`Unsubscribe` того же типа события изнутри обработчика не сломают рассылку остальным (см. раздел 8).
- **Не потокобезопасен** — рассчитан на однопоточное использование из Unity main thread.

## 3. Регистрация через Zenject

`EventBus` регистрируется как один синглтон на сцену в `Assets/_CardGame/Scripts/DI/SceneInstaller.cs`:

```csharp
private void BindEventBus()
{
    Container.Bind<IEventBus>().To<EventBus>().AsSingle();
}
```

Все потребители получают `IEventBus` **только через конструктор** (constructor injection). В проекте нет статического доступа, `Instance`-синглтона или Service Locator для EventBus — это чистый DI-паттерн.

## 4. Как объявить новое событие

События — простые immutable-классы с `readonly`-полями, лежат в `Assets/_CardGame/Scripts/Events/`. Пример:

```csharp
namespace _CardGame.Events
{
    public sealed class TargetChosenEvent
    {
        public readonly HeroView Target;

        public TargetChosenEvent(HeroView target)
        {
            Target = target;
        }
    }
}
```

Существующие события в проекте: `ActiveHeroChosenEvent`, `TargetChosenEvent`, `AttackAnimationCompletedEvent`, `TurnEndedEvent`. Для нового события создавайте по такому же образцу отдельный класс — не переиспользуйте один тип события с разными смыслами.

## 5. Как подписаться (паттерн проекта)

Подписка — в конструкторе, отписка — в `Dispose()`. Класс, который подписывается, обязан реализовывать `IDisposable`.

```csharp
public sealed class HeroesActivationStatusController : IDisposable
{
    private readonly UIService _uiService;
    private readonly IEventBus _eventBus;

    public HeroesActivationStatusController(UIService uiService, IEventBus eventBus)
    {
        _uiService = uiService;
        _eventBus = eventBus;
        _eventBus.Subscribe<ActiveHeroChosenEvent>(OnHeroChosen);
    }

    private void OnHeroChosen(ActiveHeroChosenEvent @event)
    {
        var activeHero = @event.ActiveHeroView;
        // ... реакция на событие
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<ActiveHeroChosenEvent>(OnHeroChosen);
    }
}
```

**Важно:** чтобы Zenject реально вызвал `Dispose()` при выгрузке сцены, класс нужно биндить через `BindInterfacesAndSelfTo`, а не просто `Bind`:

```csharp
Container.BindInterfacesAndSelfTo<HeroesActivationStatusController>().AsSingle().NonLazy();
```

Именно так забиндены все контроллеры, работающие с EventBus (`HeroesActivationStatusController`, `HeroAttackController`, `HeroesDeathCheckController`, `EndTurnController`).

Один класс может подписываться на несколько типов событий — просто через несколько вызовов `Subscribe` с разными перегруженными приватными обработчиками (перегрузка по типу параметра события):

```csharp
public HeroesDeathCheckController(UIService uiService, IEventBus eventBus)
{
    _eventBus = eventBus;
    _eventBus.Subscribe<AttackAnimationCompletedEvent>(OnAttackAnimationEnded);
    _eventBus.Subscribe<TurnEndedEvent>(OnAttackAnimationEnded);
}

private void OnAttackAnimationEnded(AttackAnimationCompletedEvent @event) { /* ... */ }
private void OnAttackAnimationEnded(TurnEndedEvent @event) { /* ... */ }

public void Dispose()
{
    _eventBus.Unsubscribe<AttackAnimationCompletedEvent>(OnAttackAnimationEnded);
    _eventBus.Unsubscribe<TurnEndedEvent>(OnAttackAnimationEnded);
}
```

## 6. Как опубликовать событие

`IEventBus` передаётся в конструктор (в том числе вручную создаваемым объектам, не только через DI-контейнер), и `RaiseEvent` вызывается там, где событие реально произошло:

```csharp
private void OnHeroClicked(HeroView hero)
{
    _eventBus.RaiseEvent(new TargetChosenEvent(hero));
    _taskCompletionSource.SetResult(true);
}
```

Пример ручной передачи инстанса `IEventBus` в объект, создаваемый не контейнером, а кодом (`HeroAttackController` создаёт `AttackVisualTask` вручную и прокидывает туда тот же `_eventBus`, что получил сам через DI):

```csharp
_visualPipeline.AddTask(new AttackVisualTask(currentHero, target, _eventBus));
```

## 7. Сквозной пример: реальная цепочка событий в игре

Так выглядит один полный игровой ход через EventBus:

1. Игрок кликает по герою-цели → `WaitForChooseTargetTask.OnHeroClicked` райзит `TargetChosenEvent`.
2. `HeroAttackController.OnTargetChosen` подписан на `TargetChosenEvent`, применяет способность атаки и ставит в очередь `AttackVisualTask`.
3. После проигрывания анимации `AttackVisualTask.Run` райзит `AttackAnimationCompletedEvent`.
4. На `AttackAnimationCompletedEvent` подписаны сразу два контроллера:
   - `HeroesDeathCheckController.OnAttackAnimationEnded` — проверяет, не умер ли герой;
   - `EndTurnController.OnTurnEnded` — вызывает `OnTurnEnd` у способности героя и райзит `TurnEndedEvent`.
5. На `TurnEndedEvent` снова подписан `HeroesDeathCheckController` — повторно проверяет живость героев после завершения хода.

Это удобный образец для добавления новой реакции на существующие события: достаточно засабскрайбиться на нужный тип события в конструкторе нового контроллера и не изобретать новый способ передачи данных между системами.

## 8. Пример с нуля: добавляем новое событие «исцеление героя»

Ниже — сквозной пример на условном новом событии `HeroHealedEvent`, которого пока нет в проекте. Показывает весь путь добавления фичи: создание события → его публикация там, где событие реально происходит → подписка на него в новом контроллере.

### Шаг 1. Создать класс события

`Assets/_CardGame/Scripts/Events/HeroHealedEvent.cs`

```csharp
using UI;

namespace _CardGame.Events
{
    public sealed class HeroHealedEvent
    {
        public readonly HeroView Hero;
        public readonly int HealAmount;

        public HeroHealedEvent(HeroView hero, int healAmount)
        {
            Hero = hero;
            HealAmount = healAmount;
        }
    }
}
```

### Шаг 2. Опубликовать событие

Например, способность карты, которая лечит героя, получает `IEventBus` через конструктор и райзит событие сразу после применения эффекта:

```csharp
public sealed class HealAbility
{
    private readonly IEventBus _eventBus;

    public HealAbility(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void Heal(HeroView hero, int amount)
    {
        hero.HealthData.ApplyHeal(amount);
        _eventBus.RaiseEvent(new HeroHealedEvent(hero, amount));
    }
}
```

### Шаг 3. Подписаться на событие

Новый контроллер подписывается в конструкторе и отписывается в `Dispose()` — по тому же паттерну, что и остальные подписчики в проекте (см. раздел 5):

```csharp
public sealed class HealVisualController : IDisposable
{
    private readonly IEventBus _eventBus;

    public HealVisualController(IEventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.Subscribe<HeroHealedEvent>(OnHeroHealed);
    }

    private void OnHeroHealed(HeroHealedEvent @event)
    {
        Debug.Log($"{@event.Hero.name} healed for {@event.HealAmount}");
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<HeroHealedEvent>(OnHeroHealed);
    }
}
```

### Шаг 4. Зарегистрировать контроллер в SceneInstaller

Чтобы Zenject вызвал `Dispose()` при выгрузке сцены, биндинг — через `BindInterfacesAndSelfTo` (см. раздел 5):

```csharp
Container.BindInterfacesAndSelfTo<HealVisualController>().AsSingle().NonLazy();
```

На этом цепочка замкнута: `HealAbility` не знает о `HealVisualController` и наоборот — они связаны только через тип события `HeroHealedEvent`, что и есть смысл использования EventBus вместо прямых ссылок между классами.

## 9. Найденные проблемы и рекомендации по улучшению

### Исправлено

- **Утечка подписок вне EventBus-контроллеров.** `GameManager` и `GameEndViewAdapter` реализуют `IDisposable` (у них своя подписка на обычные C#-события `Action`, не через EventBus), но в `SceneInstaller.cs` были забиндены как `Container.Bind<T>().AsSingle().NonLazy()` — без `BindInterfacesAndSelfTo`, из-за чего Zenject не вызывал их `Dispose()` при выгрузке сцены. **Исправлено:** биндинг заменён на `Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();` и аналогично для `GameEndViewAdapter`, как и у остальных EventBus-контроллеров.
- **Нет изоляции обработчиков друг от друга.** `RaiseEvent<T>` вызывал `Handler.Invoke(evt)` без try/catch — исключение в одном подписчике прерывало рассылку остальным. **Исправлено:** вызов каждого обработчика обёрнут в try/catch, исключение логируется через `Debug.LogException` и не прерывает рассылку остальным подписчикам.
- **Реентерабельность не поддерживалась.** Вызов `Subscribe<T>`/`Unsubscribe<T>` того же `T` изнутри обработчика во время `RaiseEvent` приводил к `InvalidOperationException` («Collection was modified»), так как итерировался тот же список, который мутируется. **Исправлено:** `RaiseEvent` теперь берёт снимок списка подписчиков (`list.Cast<Subscriber<T>>().ToArray()`) перед рассылкой и итерируется по нему.
- **Нет проверки на `null`.** `Subscribe`/`Unsubscribe` не проверяли `handler` на `null`. **Исправлено:** оба метода теперь просто игнорируют `null`-обработчик (`if (handler == null) return;`) вместо падения с `NullReferenceException` позже при `Invoke`.

### Осознанные ограничения (не баги, но стоит иметь в виду)

- **Диспетчеризация только по точному типу.** Подписка на базовый класс или интерфейс не получит события производных типов. Явное архитектурное ограничение текущей реализации — учитывайте это при проектировании иерархий событий.
- **Не потокобезопасен.** `Dictionary`/`List` без блокировок — safe только при вызовах из Unity main thread. Если в будущем появятся вызовы `RaiseEvent`/`Subscribe` из продолжений `UniTask`, выполняющихся не на main thread, возможны состояния гонки. Отдельно не исправлялось в рамках этой правки — потребует явного решения (лок либо запрет вызова вне main thread).
