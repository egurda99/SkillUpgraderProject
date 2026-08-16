# Hint System (Assets/_HintSystem) — гайд

Система игровых подсказок, интегрированная прямо в основной геймплей — в отличие от `Assets/_Tutorial`, здесь **нет** жёсткой пошаговой последовательности. Каждая подсказка (`HintType`) независима: появляется, когда игрок доходит до своего конкретного условия (не по индексу шага), и может завершиться в любой момент и в любом порядке относительно других подсказок.

---

## 1. Чем это отличается от `TutorialManager`

| | `TutorialManager` (`Assets/_Tutorial`) | `HintManager` (`Assets/_HintSystem`) |
|---|---|---|
| Состояние | один `_currentIndex`, один активный шаг за раз | `Dictionary<HintType, bool>` — независимый флаг на каждую подсказку |
| Порядок | строго по списку `TutorialList` | порядка нет вообще, любая подсказка может сработать в любой момент |
| Переход | `MoveToNextStep()`/`MoveToPreviousStep()` двигают индекс | `Complete(type)` просто помечает конкретный `HintType` выполненным |
| Кто реагирует | только контроллеры текущего шага (`CurrentStep == _step`) | все `HintInspector`, ожидающие именно этот `HintType`, независимо от состояния остальных |
| Параллельность | один шаг активен единовременно | сколько угодно подсказок могут одновременно "ждать" своего триггера |

Структурно `HintInspector` — почти калька с `TutorialStepControllerBase` (тот же паттерн `[Inject] Construct` → `Start()` → `Init()`), но без понятия "текущий шаг": вместо `OnNextStep`/`OnStepFinished`/`OnStepInterrupted` у `HintManager` одно событие — `OnCompleted`, и любой `HintInspector` реагирует на него сам по себе, не оглядываясь на состояние остальных.

---

## 2. Когда использовать какую систему

Это не взаимозаменяемые системы — они решают разные задачи и рассчитаны на разные сценарии, а не на "версия 1 / версия 2 одного и того же". Признак, по которому выбирать — **обязателен ли строгий порядок и линейное обучение, или подсказки нужны точечно и параллельно на протяжении всей игры**:

| Критерий | `Assets/_Tutorial` (`TutorialManager`) | `Assets/_HintSystem` (`HintManager`) |
|---|---|---|
| Когда идёт | один раз, обычно в начале игры ("онбординг") | на протяжении всей сессии/игры, сколько угодно раз |
| Порядок шагов | важен и фиксирован (`TutorialList`) | не важен, каждая подсказка независима |
| Что блокирует | обычно блокирует остальной геймплей, пока шаг не пройден (навигация/зона/попап перекрывают экран) | не должна блокировать игру — подсказка появляется/исчезает параллельно с тем, что игрок и так делает |
| Прогресс | линейный: шаг N нельзя начать, не закончив шаг N-1 | подсказки могут "стоять на паузе" сколь угодно долго и завершаться в произвольном порядке |
| Можно ли пройти повторно / вернуться назад | да, специально реализовано (`MoveToPreviousStep`, см. `TutorialModule_Guide.md`, п.8) — потому что это одна последовательность с чётким текущим положением | не применимо: нет понятия "текущего места", возвращаться некуда — просто ждём следующего наступления условия |
| Типичный пример | "Добро пожаловать → положи ресурс на конвейер → дождись переработки → забери с конвейера → прокачай конвейер → финал" | "Подсветить кнопку апгрейда, когда у игрока накопилось достаточно денег", "показать попап при первом уровне апгрейда", "подсветить кнопку карты, когда открылась новая локация" |

**Практическое правило:** если фичу можно описать как "сначала это, потом то, а до того как это не сделано — то недоступно" — это `_Tutorial`. Если фичу можно описать как "когда где-то в игре наступит X — подсветить/показать Y, а остальная игра пусть идёт своим чередом" — это `_HintSystem`. На практике обе системы обычно сосуществуют в одном проекте: `_Tutorial` проводит игрока через первый заход в игру один раз, а `_HintSystem` потом точечно подсказывает про новые механики (апгрейды, разблокировки, редко используемые кнопки) уже во время обычной игры — как в примере `UpgradeHintInspector` выше.

---

## 3. Структура модуля

```
Assets/_HintSystem/
├── Core/Scripts/
│   ├── HintType.cs              — enum всех подсказок
│   ├── HintManager.cs           — состояние (Dictionary<HintType,bool>) + событие OnCompleted
│   ├── HintInspector.cs         — абстрактная база контроллера одной подсказки
│   ├── HintManagerStarter.cs    — задаёт стартовое состояние (какие подсказки уже пройдены)
│   ├── SaveLoad/
│   │   ├── HintsData.cs         — DTO для сохранения (List<HintType> CompletedHints)
│   │   └── HintsSaveLoader.cs   — конвертер HintManager ↔ HintsData для SaveLoadManager — см. п.7
│   └── DI/
│       ├── HintManagerInstaller.cs     — Zenject-биндинг HintManager
│       └── HintsSaveLoaderInstaller.cs — биндит HintsSaveLoader как ISaveLoader — см. п.7
└── Content/UpgradeHint/Scripts/
    └── UpgradeHintInspector.cs  — рабочий пример: подсказка на кнопку апгрейда
```

### `HintManager`

```csharp
public sealed class HintManager
{
    public event Action<HintType> OnCompleted;

    public void Initialize(Dictionary<HintType, bool> hints);
    public bool IsCompleted(HintType type);
    public void Complete(HintType type); // идемпотентен: повторный вызов для уже завершённой подсказки ничего не делает и не шлёт событие повторно
    public List<HintType> GetCompletedHints(); // список завершённых типов — используется HintsSaveLoader для сохранения, см. п.7
}
```

### `HintInspector`

```csharp
public abstract class HintInspector : MonoBehaviour
{
    [SerializeField] private HintType _hintType;
    protected HintManager HintManager;

    [Inject] public void Construct(HintManager hintManager) { HintManager = hintManager; }

    public virtual void Init()
    {
        HintManager.OnCompleted += OnHintCompleted;

        if (!HintManager.IsCompleted(_hintType))
            OnStartInspect(); // подсказка ещё не пройдена — начинаем следить за условием её появления
    }

    protected abstract void OnStartInspect();  // подписаться на игровое условие / показать highlight
    protected abstract void OnFinishInspect(); // спрятать highlight, отписаться

    protected void CompleteHint() => HintManager.Complete(_hintType); // вызывать, когда цель подсказки достигнута
}
```

`OnStartInspect()` вызывается один раз при запуске сцены (если подсказка ещё не пройдена) — и именно внутри него нужно решать, **когда конкретно** подсказка должна визуально появиться (см. пример ниже — курсор появляется не сразу, а когда выполняется игровое условие).

---

## 4. Два паттерна подсказок

### Паттерн A — завершение по клику/действию игрока

Подсказка показывает highlight на элементе UI, ждёт клика, по клику — `CompleteHint()`. Полностью реализовано в `UpgradeHintInspector`:

```csharp
protected override void OnStartInspect()
{
    _cursor.SetActive(false);
    _moneyStorage.OnMoneyChanged += OnMoneyChanged; // следим, когда апгрейд станет доступен по деньгам
    _buyButton.AddListener(OnBuyButtonClicked);
    RefreshAvailability();
}

private void OnMoneyChanged(int money) => RefreshAvailability();

private void RefreshAvailability()
{
    var isAvailable = _upgradesManager.CanLevelUp(_upgradeId);
    _cursor.SetActive(isAvailable); // подсказка "появляется" именно тогда, когда апгрейд стал доступен — не раньше
}

private void OnBuyButtonClicked() => CompleteHint();
```

Здесь наглядно видно требование "подсказка появляется, когда юзер доходит до определённого этапа": курсор скрыт, пока `UpgradesManager.CanLevelUp(_upgradeId)` не станет `true` (у игрока накопилось достаточно денег), и только тогда подсвечивает кнопку. Завершается подсказка кликом по кнопке.

### Паттерн B — завершение по игровому состоянию (без клика)

Для подсказок вроде `LOG_POPUP`/`UPGRADE_POPUP` — не требующих клика, а просто реагирующих на факт события в игре (например, "показать инфо-попап при первом апгрейде"):

```csharp
public sealed class FirstUpgradePopupHintInspector : HintInspector
{
    [SerializeField] private PopupManager _popupManager;
    [SerializeField] private PopupName _popupName;

    private UpgradesManager _upgradesManager;

    [Inject]
    public void Construct(UpgradesManager upgradesManager)
    {
        _upgradesManager = upgradesManager;
    }

    protected override void OnStartInspect()
    {
        _upgradesManager.OnLevelUp += OnLevelUp;
    }

    protected override void OnFinishInspect()
    {
        _upgradesManager.OnLevelUp -= OnLevelUp;
    }

    private void OnLevelUp(Upgrade upgrade)
    {
        _popupManager.ShowPopup(_popupName);
        CompleteHint(); // одноразовая подсказка: показалась и сразу считается пройденной
    }
}
```

Разница с паттерном A только в том, что `OnStartInspect()` не показывает никакого перманентного highlight, а просто подписывается на игровое событие и сразу же в обработчике вызывает `CompleteHint()`.

### Несколько подсказок на один `HintType`

Как и в `_Tutorial` (см. `Docs/TutorialModule_Guide.md`, п.5), несколько `HintInspector` могут слушать один и тот же `HintType` — когда любой из них вызовет `CompleteHint()`, `OnFinishInspect()` сработает у **всех**, кто подписан на этот тип. Это полезно, если один и тот же прогресс должен убрать сразу несколько разных индикаторов на экране.

---

## 5. Как добавить новую подсказку

1. Добавить значение в `HintType` (`Core/Scripts/HintType.cs`).
2. Создать `Content/<Name>Hint/Scripts/<Name>HintInspector.cs` — наследник `HintInspector`, выбрать паттерн A или B (см. выше), реализовать `OnStartInspect`/`OnFinishInspect`, вызвать `CompleteHint()` там, где цель подсказки достигнута.
3. Повесить компонент на нужный GameObject на сцене, проставить `_hintType` и сериализованные ссылки в инспекторе.
4. Если инспектору нужны новые зависимости — добавить `[Inject] Construct(...)` (как в `UpgradeHintInspector`), Zenject вызовет оба `Construct`-метода (из базового класса и из наследника).
5. Ничего в `HintManager`/`HintManagerInstaller` менять не нужно — `Dictionary<HintType, bool>` в `HintManagerStarter` строится автоматически перебором всех значений `HintType` через `Enum.GetValues`.

---

## 6. Подключение к сцене (DI)

Модуль ещё не подключён ни к одной сцене — нужно руками добавить в Unity Editor:

1. На GameObject со `SceneContext` (там же, где уже висят `SceneInstaller` и `TutorialInstaller`) добавить компонент `HintManagerInstaller` и вписать его в список `Mono Installers` контекста.
2. Добавить на сцену GameObject с компонентом `HintManagerStarter` — по аналогии с `TutorialStarter`, это точка, где стартовое состояние подсказок (`_completedHints`, поле в инспекторе) передаётся в `HintManager.Initialize(...)`. Это дефолт на случай, если сохранения ещё нет — если оно есть, `HintsSaveLoader.SetupData` (см. п.7) перезатрёт этот дефолт данными из сохранения при вызове `SaveLoadManager.Load()`.
3. Повесить конкретные `HintInspector`-компоненты (например, `UpgradeHintInspector`) на соответствующие UI/игровые объекты и заполнить их поля.

---

## 7. Связь с системой сохранения (SaveLoadSystem)

Как и `Assets/_Tutorial` (см. `Docs/TutorialModule_Guide.md`, п.9), `HintManager` подключён к общему сохранятору проекта — `Assets/MyCodeBase/SaveLoad System` (тот же паттерн, что и эталонный `U:\Unity\MyCodeBase\Runtime\SaveLoadSystem`): `GameRepository` — key-value JSON-хранилище, `SaveLoader<TService, TData>` — типовой конвертер сервис↔DTO, `SaveLoadManager` — оркестратор `Save()`/`Load()` по всем `ISaveLoader`. Модуль **не вызывает `Save()`/`Load()` сам** — вызов остаётся на усмотрение конкретной сцены/бутстрапа, как и у `MoneySaveLoader`.

### Что добавлено

| Файл | Роль |
|---|---|
| `Core/Scripts/SaveLoad/HintsData.cs` | DTO: `List<HintType> CompletedHints` |
| `Core/Scripts/SaveLoad/HintsSaveLoader.cs` | `SaveLoader<HintManager, HintsData>` — конвертирует прогресс подсказок в/из DTO |
| `Core/Scripts/DI/HintsSaveLoaderInstaller.cs` | биндит `HintsSaveLoader` как `ISaveLoader` |

```csharp
public sealed class HintsSaveLoader : SaveLoader<HintManager, HintsData>
{
    protected override HintsData ConvertToData(HintManager service)
    {
        return new HintsData
        {
            CompletedHints = service.GetCompletedHints()
        };
    }

    protected override void SetupData(HintManager service, HintsData data)
    {
        var hints = new Dictionary<HintType, bool>();

        foreach (HintType hintType in Enum.GetValues(typeof(HintType)))
        {
            hints[hintType] = data.CompletedHints.Contains(hintType);
        }

        service.Initialize(hints);
    }
}
```

В отличие от `TutorialSaveLoader`, здесь `SetupData` не может просто передать DTO дальше — `HintManager.Initialize(...)` принимает полный `Dictionary<HintType, bool>` (с явным `false` для всех непройденных типов), а `HintsData` хранит только список *пройденных* типов (короче на диске, тот же принцип, что и `_completedHints` в `HintManagerStarter`). Поэтому `HintsSaveLoader.SetupData` сам достраивает словарь перебором `Enum.GetValues(typeof(HintType))` — ровно та же логика, что уже есть в `HintManagerStarter.Construct`.

`SetupDefaultData` не переопределён — без сохранения `HintManager` остаётся в состоянии, которое ему выставил `HintManagerStarter` из инспекторного `_completedHints`.

### Как подключить в сцену

1. На объект со `SceneContext` добавить `SaveSystemInstaller` (`Assets/MyCodeBase/SaveLoad System/DI/GlobalInstallers`) и `HintsSaveLoaderInstaller` в список `Mono Installers`, вместе с уже описанными в п.6 `HintManagerInstaller`/`HintManagerStarter`.
2. Добавить на сцену `SceneContainerUpdater` (общий для всех сохраняемых модулей, если уже добавлен ради `TutorialSaveLoader` — второй раз не нужен) — его `Start()` вызывает `SaveLoadManager.InitOnNewScene(container)`, которая пересобирает список `ISaveLoader` через `ResolveAll<ISaveLoader>()`.
3. Вызвать `SaveLoadManager.Load()` — по кнопке в инспекторе (`[Button]`) или программно. Порядок важен так же, как у тутора: `HintInspector.Init()` проверяет `HintManager.IsCompleted(_hintType)` **в момент своего `Start()`** (см. п.3) — если `Load()` отработает позже, подсказка на пройденном ранее объекте покажется заново до следующего `HintManager.Complete(...)`. Рекомендации по порядку вызовов те же, что в `TutorialModule_Guide.md`, п.9 (грузить на загрузочном экране до сцены с геймплеем, либо контролировать Script Execution Order).

### Что это меняет в ограничениях

Пункт "нет персистентности между сессиями" из старой версии этого раздела снят — сохранение теперь есть, если сцена настроена по инструкции выше. Оставшиеся ограничения — ниже, в п.8.

---

## 8. Ограничения текущей реализации

- **`HintManager.Complete()` необратим** — как и в `TutorialManager`, нет метода "сбросить" подсказку обратно в непройденное состояние. Если это понадобится (например, для отладки), можно добавить `Reset(HintType type)` по аналогии с `Complete`.
- **Нет валидации, что все значения `HintType` присутствуют в словаре** — `HintManager.IsCompleted` защищён через `TryGetValue` (не упадёт даже если `HintManagerStarter` не проинициализировал какое-то новое значение enum), но если `Complete()` вызвать для такого типа, он просто появится в словаре как `true` — не ошибка, но стоит держать в голове.
- **Сохранение не вызывается автоматически** — `SaveLoadManager.Save()` нужно дёрнуть явно (кнопка/автосейв/выход из игры), иначе прогресс подсказок пропадёт при перезапуске, даже если сцена настроена по п.7.
