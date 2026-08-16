# Модуль обучения (Assets/_Tutorial) — гайд по архитектуре и расширению

> Источник: `Assets/_Tutorial`. DI-часть построена на Zenject. Отдельно разобрано, как модуль можно завести в паре с `ServiceLocator` (`U:\Unity\MyCodeBase\Runtime\ServiceLocator`) — на момент написания гайда модуль **им не пользуется**, весь раздел про Service Locator (п.7) — рекомендация по интеграции, а не описание существующего кода. Обратный ход по шагам (п.8) и связь с системой сохранения (п.9), в отличие от этого, **уже реализованы** в коде модуля.

---

## 1. Общая картина

Модуль — это конечный автомат "шагов" (`TutorialStep`), которым управляет один объект `TutorialManager`. У каждого шага есть один или несколько MonoBehaviour-контроллеров на сцене (`TutorialStepControllerBase`), которые включают/выключают своё поведение по событиям менеджера.

```
TutorialStep (enum)         — список возможных шагов, их идентификаторы
TutorialList (ScriptableObject) — порядок прохождения (может отличаться от порядка в enum)
TutorialManager (POCO, Zenject-singleton) — текущий индекс, события, переходы
TutorialStepControllerBase (MonoBehaviour, abstract) — база для контроллера конкретного шага
TutorialStarter (MonoBehaviour) — точка входа, вызывает Initialize() и умеет прыгать по шагам из инспектора
```

Ключевые файлы:

| Файл | Роль |
|---|---|
| `Core/Scripts/TutorialStep.cs` | enum всех шагов |
| `Core/Scripts/TutorialList.cs` | ScriptableObject-список `TutorialStep`, задаёт реальный порядок |
| `Core/Scripts/TutorialManager.cs` | состояние тутора, события, переходы между шагами |
| `Core/Scripts/TutorialStepControllerBase.cs` | базовый класс контроллера шага |
| `Core/Scripts/TutorialStarter.cs` | инициализация `TutorialManager`, ручной переход по шагам (для отладки) |
| `Core/Scripts/DI/TutorialInstaller.cs` | Zenject-биндинги модуля |
| `Core/Scripts/SaveLoad/TutorialData.cs` | DTO для сохранения (`IsCompleted`, `CurrentIndex`) — см. п.9 |
| `Core/Scripts/SaveLoad/TutorialSaveLoader.cs` | конвертер `TutorialManager` ↔ `TutorialData` для `SaveLoadManager` — см. п.9 |
| `Core/Scripts/DI/TutorialSaveLoaderInstaller.cs` | биндит `TutorialSaveLoader` как `ISaveLoader` — см. п.9 |
| `Content/CompleteObserver/TutorialCompleteObserver.cs` | пример подписки на `OnCompleted` — открывает контент после завершения тутора |
| `Content/<N>.<Name>/...` | конкретные шаги (Config + StepController + PanelShower/Popup) |

---

## 2. TutorialManager — сердце системы

```csharp
public sealed class TutorialManager
{
    public event Action<TutorialStep> OnStepFinished;    // объективная цель шага выполнена
    public event Action<TutorialStep> OnStepInterrupted; // шаг покинут БЕЗ выполнения цели (обратный ход)
    public event Action<TutorialStep> OnNextStep;         // шаг стал активным (вход в шаг)
    public event Action OnCompleted;                      // тутор пройден целиком

    public bool IsCompleted { get; }
    public TutorialStep CurrentStep { get; }
    public int CurrentIndex { get; }
}
```

Публичные методы и их смысл:

- **`Initialize(bool isCompleted, int stepIndex)`** — вызывается один раз при старте сцены (см. `TutorialStarter.Construct`). Не рассылает `OnNextStep` для текущего шага! Контроллер сам стартует себя через `StartGame()` в `Init()` (см. ниже).
- **`FinishCurrentStep()`** — рассылает `OnStepFinished(CurrentStep)`. Это сигнал "цель шага достигнута", НЕ двигает индекс.
- **`MoveToNextStep()`** — двигает `_currentIndex` вперёд и рассылает `OnNextStep(CurrentStep)` для нового шага, либо, если шаг последний — выставляет `IsCompleted = true` и стреляет `OnCompleted`.
- **`MoveToPreviousStep()`** — обратный ход. Рассылает `OnStepInterrupted(CurrentStep)` для текущего шага (чтобы контроллер прибрал за собой), сдвигает `_currentIndex` назад и рассылает `OnNextStep(CurrentStep)` для нового текущего (уже предыдущего) шага. Подробности и правила использования — в п.8.
- **`IsStepPassed(TutorialStep step)`** — `true`, если индекс шага меньше текущего (или тутор уже завершён). Используется, чтобы контроллер уже пройденного шага не запускал себя повторно.
- **`SetStep(TutorialStep step)`** — жёсткий прыжок на произвольный шаг: меняет индекс и рассылает `OnNextStep`. Используется `TutorialStarter` через `[Button] SetStep(...)` — это ручной телепорт для отладки в инспекторе, **не** часть штатного игрового флоу: в отличие от `MoveToPreviousStep()`, он не рассылает `OnStepInterrupted`, то есть текущий активный контроллер не получает `OnStop()` и не отписывается от своих подписок при прыжке. Для отладки в play-режиме, где сцена целиком не перезапускается, используйте `MoveToPreviousStep()`/`MoveToNextStep()`, а не `SetStep()`.

Важный нюанс: `OnNextStep` по факту означает не "шаг вперёд", а **"шаг стал активным"** — это же событие используется и при `MoveToNextStep()`, и при `MoveToPreviousStep()`, и при телепорте через `SetStep()`. Контроллер не может по этому событию понять, штатно он активировался, откатился назад или его "телепортировали" — если это важно, различайте по факту вызова `CheckForInterrupt`/`CheckForStart` (см. п.3 и п.8).

---

## 3. Жизненный цикл одного шага (TutorialStepControllerBase)

```csharp
public abstract class TutorialStepControllerBase : MonoBehaviour
{
    [SerializeField] private TutorialStep _step;

    protected TutorialManager TutorialManager;

    [Inject]
    public void Construct(TutorialManager tutorialManager) { TutorialManager = tutorialManager; }

    private void Start() { Init(); }

    public virtual void Init()
    {
        TutorialManager.OnStepFinished += CheckForFinish;
        TutorialManager.OnStepInterrupted += CheckForInterrupt; // обратный ход — тоже вызывает OnStop()
        TutorialManager.OnNextStep += CheckForStart;
        StartGame(); // если шаг уже активен на момент Init — сразу вызвать OnStart()
    }

    protected virtual void OnStart() { }
    protected virtual void OnStop() { }

    protected void NotifyAboutComplete();          // FinishCurrentStep()
    protected void NotifyAboutMoveNext();           // MoveToNextStep()
    protected void NotifyAboutCompleteAndMoveNext(); // оба сразу
}
```

`OnStop()` теперь вызывается в двух разных ситуациях: когда цель шага достигнута (`OnStepFinished`) и когда шаг прерван обратным ходом (`OnStepInterrupted`, см. п.8). Поэтому `OnStop()` — это не просто "почистить после успеха", а полное симметричное завершение шага: спрятать панель/зону/стрелку и отписаться от всех событий, на которые подписались в `OnStart()`, **независимо от того, была ли достигнута цель**.

Схема одного "простого" шага (одна цель — один контроллер):

```
TutorialManager.OnNextStep(step)
        │  (совпал _step)
        ▼
   OnStart()  — показать панель подсказки, стрелку навигации, зону, подписаться на игровое событие
        │
        │  игрок выполнил действие (вошёл в зону, нажал кнопку, апгрейд получен...)
        ▼
   NotifyAboutCompleteAndMoveNext()
        │
        ├─ TutorialManager.FinishCurrentStep() → OnStepFinished(step) → CheckForFinish → OnStop()
        │      (спрятать панель/зону, отписаться от игровых событий)
        │
        └─ TutorialManager.MoveToNextStep() → индекс++ → OnNextStep(next) → следующий контроллер OnStart()
```

`CheckForStart`/`CheckForFinish` в базовом классе просто сверяют, относится ли пришедшее событие к `_step` этого конкретного контроллера — то есть **все** контроллеры на сцене подписаны на **все** события менеджера, но реагируют только на "свои".

### Типовая структура папки шага

На примере `2.AddResourcesToConveyor`:

```
2.AddResourcesToConveyor/
├── Config «AddResourcesToConveyorConfig».asset   — ScriptableObject с текстом/иконкой/ссылками
└── Scripts/
    ├── AddResourcesToConveyorConfig.cs            — данные шага (title, description, sprite, PanelShower)
    ├── MoveToConveyorPanelShower.cs                — [Serializable], наследует InfoPanelShower, кладёт текст на UI-панель подсказки
    └── AddResourcesToConveyorStepController.cs     — TutorialStepControllerBase, связывает всё вместе
```

`Config` хранит **и данные, и вложенный `[Serializable]` Shower** — это позволяет держать вместе "что показать" и "как показать", а сам `PanelShower` переиспользует общий `MyCodeBase.UI.InfoPanelShower` (`Show(parent)` / `Hide()` + `OnShow`/`OnHide` хуки).

Если у шага есть попап — используется MVP-триада: `Popup : MyCodeBase.Popup` (View-контейнер) + `Presenter` (обычный POCO, привязывает конфиг к вью) + `View` (чистый UI без логики), см. `WelcomePopup` / `WelcomePresenter` / `WelcomeView`.

---

## 4. Как добавить новый шаг — пошагово

1. **Enum** `Core/Scripts/TutorialStep.cs` — добавить новое значение (можно в конец, порядковое число значения не обязано совпадать с порядком прохождения).
2. **`TutorialList.asset`** — открыть ассет в инспекторе и вставить новый `TutorialStep` в нужную позицию списка. Порядок прохождения задаётся **этим списком**, а не порядком в enum — так можно, например, поменять местами шаги или временно исключить шаг без правки enum.
3. **Папка контента** `Content/N.NewStepName/` со структурой:
   - `Scripts/NewStepConfig.cs` — `ScriptableObject`, поля с текстом/спрайтами/ссылками на префабы/`PopupName` и т.д.
   - при необходимости UI-подсказки: `NewStepPanelShower.cs` — `[Serializable]`, наследник `InfoPanelShower`, переопределяет `OnShow`/`OnHide`.
   - `NewStepController.cs` — наследник `TutorialStepControllerBase`:
     - в `[Inject] Construct(...)` получить нужные зависимости (`NavigationManager`, `VisualZoneManager`, `PopupManager`, игровые системы и т.п.);
     - в `OnStart()` — показать подсказку/зону/стрелку, подписаться на игровое событие-триггер;
     - в обработчике игрового события — спрятать UI/зону, отписаться, вызвать `NotifyAboutCompleteAndMoveNext()` (или раздельно, см. п.5);
     - в `OnStop()` — обязательно отписаться от **всех** подписок, сделанных в `OnStart()` (симметрично).
   - `Config «NewStepName».asset` — создать через `CreateAssetMenu` (`Tutorial/New Config «...»`) и заполнить в инспекторе.
4. **Сцена** `Tutor_UpgradeScene.unity` — добавить GameObject с компонентом `NewStepController`, назначить `_step` = новое значение enum, проставить сериализованные ссылки (`_config`, `_panelContainer`, `_targetPosition` и т.п.).
5. **DI**, если контроллеру нужна новая зависимость, которой раньше не было — добавить биндинг в `TutorialInstaller.InstallBindings()`.
6. **Проверка** — через `TutorialStarter` в инспекторе (кнопка `SetStep`) телепортироваться на новый шаг и убедиться, что `OnStart`/`OnStop` отрабатывают корректно.

### Мини-шаблон нового контроллера

```csharp
public sealed class NewStepController : TutorialStepControllerBase
{
    [SerializeField] private NewStepConfig _config;
    [SerializeField] private Transform _panelContainer;

    private NewStepPanelShower _panelShower;
    private SomeGameplaySystem _gameplaySystem;

    [Inject]
    public void Construct(SomeGameplaySystem gameplaySystem)
    {
        _gameplaySystem = gameplaySystem;
        _panelShower = _config.PanelShower;
        _panelShower.Init(_config);
    }

    protected override void OnStart()
    {
        _gameplaySystem.OnGoalReached += OnGoalReached;
        _panelShower.Show(_panelContainer);
    }

    protected override void OnStop()
    {
        base.OnStop();
        _gameplaySystem.OnGoalReached -= OnGoalReached;
    }

    private void OnGoalReached()
    {
        _panelShower.Hide();
        NotifyAboutCompleteAndMoveNext();
    }
}
```

---

## 5. Несколько контроллеров на один `TutorialStep`

Один логический шаг может состоять из **нескольких** MonoBehaviour-контроллеров с одним и тем же значением `_step` — так сделан шаг `UPGRADE_CONVEYOR` (папка `5.UpgradeConveyor`):

- `MoveToUpgradePointStepController` — фаза "дойди до точки апгрейда": показывает стрелку/зону, ждёт `PlaceTriggerPoint.OnPlaceVisited`, затем прячет зону и открывает попап апгрейда.
- `UpgradeConveyorPopupStepController` — фаза "прокачай конвейер в попапе": ждёт события апгрейда (`UpgradeQuestInspector`/`OnLevelUp`), затем ждёт закрытия попапа.

Оба подписаны на `TutorialManager.OnNextStep`/`OnStepFinished`, у обоих `_step == UPGRADE_CONVEYOR`, поэтому оба получают `OnStart()` в один момент, когда менеджер входит в этот шаг — но каждый следит за своей частью логики.

Здесь и раскрывается разница трёх Notify-методов:

| Метод | Что делает | Когда использовать |
|---|---|---|
| `NotifyAboutComplete()` | только `FinishCurrentStep()` → `OnStepFinished` → `OnStop()` текущего контроллера | Промежуточная фаза шага завершена, но индекс двигать рано — следующая фаза должна сама решить, когда шагать дальше (см. `UpgradeConveyorPopupStepController.OnUpgraded`) |
| `NotifyAboutMoveNext()` | только `MoveToNextStep()` | Индекс нужно сдвинуть, но `OnStepFinished`/`OnStop` уже были вызваны раньше (например, той же фазой) — типично для второй половины двухфазного шага (см. `OnCloseClicked`) |
| `NotifyAboutCompleteAndMoveNext()` | оба сразу | Стандартный одношаговый контроллер — 90% случаев |

**Правило при добавлении многофазного шага**: используйте одно и то же значение `_step` у всех контроллеров-фаз, а `NotifyAboutComplete()`/`NotifyAboutMoveNext()` распределяйте так, чтобы `FinishCurrentStep()` вызывался ровно один раз за прохождение шага (иначе `OnStop()` всех контроллеров этого шага отработает раньше времени).

---

## 6. Работа через Zenject (текущая реализация)

`TutorialInstaller : MonoInstaller<TutorialInstaller>` — единственная точка биндингов модуля:

```csharp
Container.Bind<TutorialManager>().AsSingle().WithArguments(_stepList);
Container.Bind<UpgradeTriggerPoint>().FromComponentInHierarchy().AsSingle();
Container.BindInterfacesAndSelfTo<NavigationManager>().AsSingle().WithArguments(...);
Container.BindInterfacesAndSelfTo<VisualZoneManager>().AsSingle().WithArguments(...);
Container.BindInterfacesAndSelfTo<TutorialCompleteObserver>().AsSingle();
```

- `TutorialManager` — обычный POCO, не MonoBehaviour, регистрируется как singleton с аргументом `TutorialList` (ScriptableObject передаётся через `WithArguments`, а не инжектится напрямую — так конфигурация тутора становится частью композиции, а не глобальным ассетом, на который завязан весь код).
- `TutorialStepControllerBase` — MonoBehaviour на сцене, получает `TutorialManager` через метод-инъекцию `[Inject] Construct(...)`. Zenject вызывает `Construct` **до** `Start()` Unity, поэтому к моменту `Init()` (вызывается из `Start()`) зависимость уже проставлена.
- `NavigationManager`/`VisualZoneManager` реализуют `IInitializable`/`ITickable` — Zenject сам вызывает их `Initialize()`/`Tick()` через `SceneContext`, без ручного вызова откуда-либо ещё.
- `TutorialCompleteObserver` — чистый POCO-подписчик (`IDisposable`), пример "слушателя" тутора без MonoBehaviour: включает `UpgradeTriggerPoint` после `OnCompleted`.

Из этого следует стандартный способ подписаться на тутор откуда угодно в DI-графе: попросить `TutorialManager` в конструкторе/`[Inject] Construct`, как это делает `TutorialCompleteObserver`.

---

## 7. Работа в паре с ServiceLocator (`Digitech.ServiceLocator`)

На данный момент `Assets/_Tutorial` **не использует** `ServiceLocator` — весь модуль целиком на Zenject. Ниже — как модуль устроен в `MyCodeBase` и как его можно подключить к тутору, если понадобится доступ к `TutorialManager` вне DI-графа Zenject (легаси-код, редакторские утилиты, объекты, которые Zenject не спавнит, кросс-сценовые системы).

### Что уже есть в `ServiceLocator`

```csharp
namespace Digitech.ServiceLocator
{
    public sealed class ServiceLocator      // "сессионный"/сценовый локатор
    {
        public static ServiceLocator Instance { get; }
        public void Register<T>(object service);
        public T Get<T>();
        public bool TryGet<T>(out T service);
        // если сервис не найден локально — падает fallback на GlobalServiceLocator.Instance
    }

    public sealed class GlobalServiceLocator  // персистентный локатор (DontDestroyOnLoad)
    {
        public static GlobalServiceLocator Instance { get; }
        // тот же API, без fallback
    }
}
```

- `ServiceLocatorInstaller` (MonoBehaviour) очищает `ServiceLocator.Instance` в `OnDestroy` — то есть локатор живёт в рамках сцены/сессии.
- `GlobalServiceLocatorInstaller` — синглтон-бутстрап с `DontDestroyOnLoad`, очищает `GlobalServiceLocator.Instance` при первом запуске (защита от дублей через `_instance`).
- `ServiceLocator.Get<T>()`/`Contains<T>()` **автоматически** ищут в `GlobalServiceLocator`, если не нашли локально — то есть сценовый локатор прозрачно "видит" глобальные сервисы.

### Рекомендуемая схема интеграции — гибрид, а не замена

Zenject остаётся основным способом собрать граф зависимостей модуля (это соответствует общему правилу проекта — избегать синглтонов и раздавать зависимости через конструктор). `ServiceLocator` стоит использовать точечно — как "мост" наружу, туда, куда Zenject-контейнер не дотягивается.

**Вариант A — регистрация как побочный эффект DI (рекомендуется).**
Добавить в `TutorialInstaller` небольшой bridge-класс, который после создания `TutorialManager` регистрирует его в локаторе:

```csharp
public sealed class TutorialServiceLocatorBridge : IInitializable, IDisposable
{
    private readonly TutorialManager _tutorialManager;

    public TutorialServiceLocatorBridge(TutorialManager tutorialManager)
    {
        _tutorialManager = tutorialManager;
    }

    public void Initialize()
    {
        ServiceLocator.Instance.Register<TutorialManager>(_tutorialManager);
    }

    public void Dispose()
    {
        ServiceLocator.Instance.Unregister<TutorialManager>();
    }
}
```

```csharp
// TutorialInstaller.InstallBindings()
Container.BindInterfacesTo<TutorialServiceLocatorBridge>().AsSingle();
```

Дальше любой код, у которого нет доступа к Zenject-контейнеру (например, редакторский инструмент, ассет-процессор, объект из другой сцены без `SceneContext`), получает тот же экземпляр менеджера:

```csharp
if (ServiceLocator.Instance.TryGet(out TutorialManager tutorialManager))
{
    tutorialManager.MoveToNextStep();
}
```

**Вариант B — ServiceLocator как замена Zenject-инъекции в `TutorialStepControllerBase`.**
Более радикальный вариант — вообще убрать `[Inject]` и получать `TutorialManager` через локатор в `Awake`:

```csharp
private void Awake()
{
    if (ServiceLocator.Instance.TryGet(out TutorialManager tutorialManager))
        TutorialManager = tutorialManager;
}
```

Это упрощает бутстрап (не нужен `SceneContext`/`MonoInstaller` для самого тутора), но:
- теряется явность зависимостей (конструктор/`[Inject]` перестают документировать, что нужно классу);
- сложнее тестировать контроллер изолированно (нужен глобальный статический `Instance`, а не подставной объект через `WithArguments`);
- нужно гарантировать порядок инициализации вручную (что кто-то зарегистрировал `TutorialManager` в локаторе **до** первого `Awake()` контроллера — Zenject такой порядок обеспечивает сам через граф зависимостей, локатор — нет).

**Вывод:** для внутренних связей модуля (контроллер шага ↔ `TutorialManager`, менеджеры навигации/зон) оставить Zenject как есть. `ServiceLocator`/`GlobalServiceLocator` подключать только там, где реально нет доступа к контейнеру — по Варианту A, точечно, с явной регистрацией/дерегистрацией через `IInitializable`/`IDisposable`, не размазывая `ServiceLocator.Instance.Get<T>()` по всем контроллерам шагов.

---

## 8. Обратный ход по шагам (реализовано)

Модуль умеет ходить назад: `TutorialManager.MoveToPreviousStep()`. Ниже — как это устроено и как этим пользоваться.

### Что добавлено в `TutorialManager.cs`

Новое событие `OnStepInterrupted` (шаг покинут **без** выполнения цели — в отличие от `OnStepFinished`, который означает "цель достигнута") и симметричный `MoveToNextStep()` метод:

```csharp
public event Action<TutorialStep> OnStepInterrupted;

public void MoveToPreviousStep()
{
    if (_isCompleted)
    {
        return;
    }

    if (_currentIndex <= 0)
    {
        return;
    }

    OnStepInterrupted?.Invoke(CurrentStep);

    _currentIndex--;
    OnNextStep?.Invoke(CurrentStep);
}
```

Правила поведения:

- Если тутор уже завершён (`IsCompleted == true`) — назад ходить нельзя, метод ничего не делает. Это осознанное ограничение: откат состояния после `OnCompleted` (когда уже могли сработать необратимые эффекты вроде `TutorialCompleteObserver`, включающего `UpgradeTriggerPoint`) — отдельная продуктовая задача, а не техническая, в рамках этой реализации не решается.
- Если уже стоите на первом шаге (`_currentIndex <= 0`) — назад ходить некуда, метод ничего не делает.
- Иначе: сначала рассылается `OnStepInterrupted(CurrentStep)` для **текущего** (ещё активного) шага — это даёт его контроллеру шанс прибрать за собой, — и только потом индекс сдвигается назад и рассылается `OnNextStep(CurrentStep)` для нового (уже предыдущего) шага, который переигрывает свой `OnStart()`.

### Что добавлено в `TutorialStepControllerBase.cs`

Третий обработчик, симметричный `CheckForFinish`, — тоже вызывает `OnStop()`, но по причине "прервали", а не "закончили":

```csharp
public virtual void Init()
{
    TutorialManager.OnStepFinished += CheckForFinish;
    TutorialManager.OnStepInterrupted += CheckForInterrupt;
    TutorialManager.OnNextStep += CheckForStart;
    StartGame();
}

private void OnDestroy()
{
    TutorialManager.OnStepFinished -= CheckForFinish;
    TutorialManager.OnStepInterrupted -= CheckForInterrupt;
    TutorialManager.OnNextStep -= CheckForStart;
}

private void CheckForInterrupt(TutorialStep step)
{
    if (_step == step)
    {
        OnStop();
    }
}
```

Ничего больше в базовом классе менять не пришлось — `OnStop()` каждого конкретного контроллера и так вызывается по тому же принципу, что и при штатном завершении шага.

### Как включить обратный ход у себя (для отладки/UI)

Для ручной проверки в инспекторе в `TutorialStarter` добавлена кнопка:

```csharp
[Button]
public void MoveToPreviousStep()
{
    _tutorialManager.MoveToPreviousStep();
}
```

В плей-режиме выделите объект с `TutorialStarter` на сцене `Tutor_UpgradeScene` и нажмите `Move To Previous Step` в инспекторе (Odin) — текущий шаг корректно свернётся (`OnStop()`), а предыдущий переактивируется (`OnStart()`).

Для реальной UI-кнопки "Назад" в интерфейсе — заведите отдельный тонкий MonoBehaviour, получающий `TutorialManager` через Zenject, и повесьте вызов на `OnClick` кнопки:

```csharp
public sealed class TutorialBackButtonController : MonoBehaviour
{
    private TutorialManager _tutorialManager;

    [Inject]
    public void Construct(TutorialManager tutorialManager)
    {
        _tutorialManager = tutorialManager;
    }

    public void OnBackButtonClicked()
    {
        _tutorialManager.MoveToPreviousStep();
    }
}
```

Это не часть базового модуля (в проекте пока нет самой UI-кнопки "Назад"), но добавляется в одну строку поверх готового `MoveToPreviousStep()`.

### Что пришлось поправить в контроллерах шагов, чтобы обратный ход реально работал

Раньше зона/стрелка навигации/панель-подсказка прятались **только** внутри обработчика успешного завершения шага (например, `OnConverterVisited`), а не в `OnStop()`. При штатном прохождении это работало, потому что обработчик сам вызывал `Hide()`/`HideZone()`/`Stop()` перед `NotifyAbout...`. Но если шаг прерывается обратным ходом **до** того, как цель достигнута, `OnStop()` вызывается напрямую — и без явного скрытия в нём UI/зона/стрелка так и остались бы висеть на экране. Это исправлено ходом задачи — теперь `OnStop()` каждого контроллера сам прячет всё, что показал `OnStart()`, независимо от причины (`OnStepFinished` или `OnStepInterrupted`):

- `AddResourcesToConveyorStepController`, `TakeFromConveyorStepController`, `MoveToUpgradePointStepController` (шаг 5, фаза 1), `FinalStepController` (шаг 6, фаза 1) — `OnStop()` теперь дополнительно вызывает `_visualZoneManager.HideZone()`, `_navigationManager.Stop()` и `Hide()` соответствующего `PanelShower`.
- `WaitConvertationStepController` — `OnStop()` дополнительно прячет `_waitConvertationPanelShower`.
- `MoveToUpgradePointStepController` / `FinalStepController` — созданный в `OnStart()` через `Instantiate(_config.PlaceTriggerPointPrefab, ...)` триггер теперь уничтожается в `OnStop()` (`Destroy(_triggerPoint.gameObject)`), а не только отписывается — иначе при повторных проходах вперёд-назад-вперёд на сцене копились бы неиспользуемые `PlaceTriggerPoint`.
- `UpgradeConveyorPopupStepController` (шаг 5, фаза 2, многофазный) — `OnStop()` был вообще пустым. Теперь снимает слушатель с `_closeButton`, останавливает `UpgradeQuestInspector` (добавлен публичный `Stop()`, отписывающий `OnLevelUp`), прячет попап апгрейда, если он был открыт, и возвращает курсоры/кнопку в исходное неактивное состояние — то же, что задаётся в `Awake()`. Без этого, если прервать шаг между открытием попапа и его закрытием, слушатель кнопки и подписка на апгрейд дублировались бы при повторном входе в шаг.
- `FinalPopupStepController` — `OnStop()` тоже был пустым; добавлено снятие слушателя с `_closeButton` и скрытие попапа, если он ещё активен.
- Заодно поправлена опечатка в `TakeFromConveyorStepController.OnStop()`: отписка шла от `OnInputChanged`, хотя подписка в `OnStart()` — на `OnOutputChanged` (см. было в п.10 как баг, теперь исправлено).

### На что обратить внимание, если добавляете новый многофазный шаг с расчётом на обратный ход

1. **`OnStop()` — это не только "отписаться".** Он должен полностью откатывать всё, что делает `OnStart()`: прятать UI/зону/стрелку, уничтожать разово заспавненные объекты, сбрасывать визуальное состояние кнопок/курсоров к тому, что было до `OnStart()`.
2. **Многофазные шаги (см. п.5)** требуют по чек-листу пройтись по каждой фазе отдельно: если одна фаза уже вызвала `NotifyAboutComplete()`, а игрок идёт назад в шаг, при повторном `OnStart()` обеих фаз стоит проверять реальное состояние геймплея (как сделано неявно через `UpgradesManager` в `UpgradeQuestInspector`), а не слепо повторять весь сетап с нуля.
3. **`SetStep()` по-прежнему не рассылает `OnStepInterrupted`** — это осознанно оставлено как есть, потому что `SetStep()` используется только для телепорта в отладке (обычно сразу после которого сцена перезапускается). Если понадобится безопасный "прыжок" в рантайме без перезапуска сцены — используйте цепочку `MoveToPreviousStep()`/`MoveToNextStep()`, а не `SetStep()`.

---

## 9. Связь с системой сохранения (SaveLoadSystem)

Модуль подключён к общему сохранятору проекта — `Assets/MyCodeBase/SaveLoad System` (тот же паттерн, что и эталонный `U:\Unity\MyCodeBase\Runtime\SaveLoadSystem`, откуда он изначально взят): `IGameRepository`/`GameRepository` — key-value хранилище с JSON-сериализацией под капотом (ключ — имя типа DTO), `SaveLoader<TService, TData>` — типовой конвертер сервис↔DTO, `SaveLoadManager` — оркестратор, вызывающий `Save()`/`Load()` разом для всех зарегистрированных `ISaveLoader`. Как и остальной проект (см. пример `MoneySaveLoader`), модуль **не вызывает `Save()`/`Load()` сам** — это ручной/точечный вызов на усмотрение конкретной сцены/бутстрапа.

### Что добавлено

| Файл | Роль |
|---|---|
| `Core/Scripts/SaveLoad/TutorialData.cs` | DTO: `bool IsCompleted`, `int CurrentIndex` |
| `Core/Scripts/SaveLoad/TutorialSaveLoader.cs` | `SaveLoader<TutorialManager, TutorialData>` — конвертирует состояние тутора в/из DTO |
| `Core/Scripts/DI/TutorialSaveLoaderInstaller.cs` | биндит `TutorialSaveLoader` как `ISaveLoader` (`BindInterfacesAndSelfTo`) |

```csharp
public sealed class TutorialSaveLoader : SaveLoader<TutorialManager, TutorialData>
{
    protected override TutorialData ConvertToData(TutorialManager service)
    {
        return new TutorialData
        {
            IsCompleted = service.IsCompleted,
            CurrentIndex = service.CurrentIndex
        };
    }

    protected override void SetupData(TutorialManager service, TutorialData data)
    {
        service.Initialize(data.IsCompleted, data.CurrentIndex);
    }
}
```

`SetupDefaultData` не переопределён: если сохранения ещё нет, `TutorialManager` остаётся в состоянии, которое ему уже выставил `TutorialStarter.Construct` из инспекторных полей `_isCompleted`/`_stepIndex` — это и есть дефолт (аналогично тому, как `MoneySaveLoader.SetupDefaultData` явно выставляет `100`, только здесь дефолт задан раньше по DI-цепочке, дублировать его незачем).

`SetupData` просто повторно зовёт `Initialize(...)` — это единственный метод `TutorialManager`, который атомарно выставляет и `_isCompleted`, и `_currentIndex` (плюс рассылает `OnCompleted`, если сохранённое состояние — "тутор пройден").

### Как подключить в сцену

1. На объект со `SceneContext` (тот же, где висит `TutorialInstaller`) добавить `SaveSystemInstaller` (`Assets/MyCodeBase/SaveLoad System/DI/GlobalInstallers`, биндит `GameRepository`+`SaveLoadManager`) и `TutorialSaveLoaderInstaller` в список `Mono Installers`. `SaveSystemInstaller` подключается один раз на сцену/проект; свой `*SaveLoaderInstaller` — на каждый сохраняемый модуль (тутор, подсказки, деньги и т.д.).
2. Добавить на сцену `SceneContainerUpdater` (если его там ещё нет) — в `Start()` он вызывает `SaveLoadManager.InitOnNewScene(container)`, которая пересобирает список `ISaveLoader` через `container.ResolveAll<ISaveLoader>()`. Без этого шага `TutorialSaveLoader` не попадёт в рабочий список `SaveLoadManager`.
3. Вызвать `SaveLoadManager.Load()`/`Save()` — вручную через Odin-кнопки в инспекторе (`SaveLoadManager` помечен `[Button]` на обоих методах) либо программно из своего bootstrap-кода/UI (кнопка "Сохранить", автосейв по таймеру, `OnApplicationPause` и т.п. — на усмотрение проекта).

### Важный нюанс по порядку вызовов

`TutorialStepControllerBase.Start()` читает `TutorialManager.CurrentStep`/`IsStepPassed` **в момент своего собственного `Start()`** (см. `StartGame()`, п.3) — то есть чтобы восстановленный из сохранения шаг корректно "подхватился" контроллером (показал нужную панель/зону), `SaveLoadManager.Load()` должен успеть отработать **до** `Start()` контроллеров шагов. Unity не гарантирует порядок `Start()` между разными компонентами сцены сам по себе.

Практические варианты (модуль это не решает сам — это конфигурация конкретной сцены/загрузочного экрана):
- вызывать `SaveLoadManager.Load()` на отдельном загрузочном экране/сцене **до** активации сцены с тутором;
- задать Script Execution Order так, чтобы компонент, вызывающий `Load()`, гарантированно стартовал раньше любых `TutorialStepControllerBase`-наследников;
- либо явно "переиграть" текущий шаг после `Load()` — в `TutorialManager` сейчас нет отдельного метода вроде `RefreshCurrentStep()` для этого случая (он не нужен, пока `Load()` вызывается раньше `Start()` контроллеров), но это возможное расширение, если понадобится безопасный повторный вызов `OnNextStep` после позднего `Load()`.

### Что сохраняется, а что нет

- Сохраняются: пройден ли тутор целиком (`IsCompleted`) и на каком шаге игрок остановился (`CurrentIndex`).
- НЕ сохраняется состояние конкретной фазы внутри многофазного шага (см. п.5) — например, если игрок уже нажал "апгрейд" в попапе, но не закрыл его, после `Load()` шаг начнётся заново с `OnStart()` текущей фазы, а не с точного места посреди попапа. Для короткого обучающего сценария это приемлемо; если станет проблемой — `TutorialData`/`ConvertToData`/`SetupData` расширяются под конкретную фазу.

---

## 10. Замеченные особенности и потенциальные проблемы

При разборе модуля нашлось несколько мест, которые стоит перепроверить (не факт, что баги, но выглядят подозрительно). Опечатка в `TakeFromConveyorStepController.OnStop()` и пустые `OnStop()` в двухфазных попап-контроллерах уже исправлены по ходу добавления обратного хода (см. п.8) — здесь оставлено то, что не трогалось:

- **`FinalStepController.OnPlaceVisited()`** вызывает `_popupManager.ShowPopup(_config.PopupName);` дважды подряд (один раз внутри `if (_popup is FinishPopup finishPopup)`, второй раз — сразу после `NotifyAboutComplete()`, уже безусловно). Второй вызов избыточен (`PopupManager.ShowPopup` сам по себе идемпотентен благодаря проверке `IsPopupActive`, так что бага на рантайме не будет, но код вводит в заблуждение).

- **`TutorialStarter`** хранит закомментированный `Start()` — мёртвый код, можно убрать при следующей правке файла.

- **`SetStep()` не проверяет границы индекса.** `IndexOfStep` при отсутствии шага в `TutorialList` вернёт `-1`, что уйдёт в `_currentIndex = -1` и дальше сломает `CurrentStep` (`IndexOutOfRange` на `_stepList[-1]`). Стоит либо валидировать `TutorialList.Contains(step)` перед прыжком, либо убедиться, что все значения enum всегда присутствуют в ассете `TutorialList`.

- **`ConfigConverter`/имена ассетов** используют кавычки-«ёлочки» в именах файлов (`Config «Welcome».asset`) — осознанный проектный нейминг-конвеншен, не ошибка, но при автоматизации (скрипты импорта, CI) может потребовать аккуратной работы с путями.

---

## 11. Чек-лист "добавить новый шаг" (короткая версия)

- [ ] Новое значение в `TutorialStep` enum
- [ ] Новое значение вставлено в нужную позицию `TutorialList.asset`
- [ ] `Config` (ScriptableObject) + при необходимости `PanelShower`/`Popup`+`Presenter`+`View`
- [ ] `StepController : TutorialStepControllerBase` с `OnStart`/`OnStop`, симметричные подписки/отписки
- [ ] Выбран правильный Notify-метод (`Complete` / `MoveNext` / `CompleteAndMoveNext`) — см. таблицу в п.5
- [ ] GameObject на сцене, `_step` проставлен в инспекторе, ссылки на `_config`/`_panelContainer`/`_targetPosition` заполнены
- [ ] Новые зависимости добавлены в `TutorialInstaller`, если нужно
- [ ] Проверено через `TutorialStarter.SetStep` в инспекторе
- [ ] `OnStop()` полностью откатывает `OnStart()` (прячет UI/зону/стрелку, уничтожает разово заспавненные объекты, сбрасывает состояние кнопок/курсоров) — не только отписывается от событий
- [ ] Проверено через `TutorialStarter.MoveToPreviousStep` в инспекторе, что шаг корректно сворачивается при обратном ходе (см. п.8) и не плодит дубликаты (`Instantiate` и т.п.) при повторном входе
- [ ] Если новый шаг добавляет собственное сохраняемое состояние сверх `IsCompleted`/`CurrentIndex` — расширены `TutorialData`/`TutorialSaveLoader` (см. п.9), иначе после `Load()` шаг откатится к своему `OnStart()`, а не к точному прогрессу внутри шага
