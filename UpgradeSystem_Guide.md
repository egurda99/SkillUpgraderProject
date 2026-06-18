# Upgrade System — Руководство разработчика

## Содержание

1. [Общий обзор](#общий-обзор)
2. [Архитектура](#архитектура)
3. [Как работает система](#как-работает-система)
4. [API](#api)
5. [Как добавить новый апгрейд](#как-добавить-новый-апгрейд)
6. [Как добавить апгрейды для новой сущности](#как-добавить-апгрейды-для-новой-сущности)
7. [Биндинг в Zenject](#биндинг-в-zenject)

---

## Общий обзор

Система апгрейдов позволяет улучшать параметры конвертера (входная/выходная вместимость, скорость конвертации) за внутриигровую валюту. Каждый апгрейд имеет несколько уровней с таблицей значений и таблицей цен.

**Ключевые характеристики:**
- Данные апгрейда хранятся в `ScriptableObject`-конфигах
- Логика апгрейда — в обычных C#-классах (не MonoBehaviour)
- Зависимости подключаются через Zenject
- UI строится автоматически по каталогу конфигов
- Первый уровень каждого апгрейда применяется бесплатно при старте игры

**Существующие апгрейды:**

| Апгрейд | Класс | Что улучшает |
|---|---|---|
| Input Capacity | `InputCapacityUpgrade` | Вместимость входной зоны конвертера |
| Output Capacity | `OutputCapacityUpgrade` | Вместимость выходной зоны конвертера |
| Time Convertation | `TimeConvertationUpgrade` | Время одной конвертации |

---

## Архитектура

```
UpgradeCatalog (ScriptableObject)
  └── UpgradeConfig[] (ScriptableObject, по одному на каждый апгрейд)
        ├── UpgradePriceTable   — цены по уровням
        ├── UpgradeMetaData     — иконка, название, описание
        └── ValueTable          — значения параметра по уровням (CapacityTable / TimeConvertationTable)

UpgradesManager
  └── Dictionary<string, Upgrade>
        └── Upgrade (plain C# class)
              └── применяет эффект в ConverterData через ConverterDataService

UI (MVP)
  ConverterUpgradesPopup
    └── foreach config → UpgradePresenter + UpgradeView
```

### Основные классы

| Класс | Тип | Путь | Назначение |
|---|---|---|---|
| `Upgrade` | abstract class | `UpgradesSystem/Base/Upgrade.cs` | Базовый класс поведения апгрейда |
| `UpgradeConfig` | abstract ScriptableObject | `UpgradesSystem/Base/UpgradeConfig.cs` | Базовый класс конфига апгрейда |
| `UpgradeCatalog` | ScriptableObject | `UpgradesSystem/Base/UpgradeCatalog.cs` | Список всех конфигов |
| `UpgradePriceTable` | struct | `UpgradesSystem/Base/UpgradePriceTable.cs` | Таблица цен по уровням |
| `IUpgradeMetadata` | interface | `UpgradesSystem/Base/IUpgradeMetadata.cs` | Иконка, название, описание |
| `UpgradesManager` | sealed class | `UpgradesSystem/UpgradeManager.cs` | Менеджер: покупка, словарь апгрейдов |
| `CapacityTable` | struct | `UpgradesSystem/ConverterUpgrades/Capacity/CapacityTable.cs` | Экспоненциальная таблица ёмкостей |
| `TimeConvertationTable` | struct | `UpgradesSystem/ConverterUpgrades/TimeConvertation/TimeConvertationTable.cs` | Линейная таблица времени |
| `UpgradePresenter` | sealed class | `UI/ConveterUpgradesPopup/UpgradePresenter.cs` | Презентер (MVP) |
| `UpgradeView` | MonoBehaviour | `UI/ConveterUpgradesPopup/UpgradeView.cs` | View (MVP) |
| `ConverterUpgradesPopup` | MonoBehaviour | `UI/ConveterUpgradesPopup/ConverterUpgradesPopup.cs` | Попап со списком апгрейдов |

---

## Как работает система

### Инициализация (при старте сцены)

```
SceneInstaller.InstallBindings()
  │
  ├─ new ConverterData(exchangeRates)
  ├─ bind ConverterDataService (singleton)
  ├─ bind MoneyStorage (singleton)
  └─ bind UpgradesManager (singleton) с аргументом UpgradeCatalog
        │
        └─ UpgradesManager ctor → Setup(catalog.GetAllUpgrades())
              │
              foreach config in catalog:
                ├─ upgrade = config.Create()           // new ConcreteUpgrade(config)
                ├─ container.Inject(upgrade)            // вызывает upgrade.Construct(...)
                │     └─ применяет значение Level=1 в ConverterData (бесплатно)
                └─ _upgrades[config.Id] = upgrade
```

### Покупка апгрейда (по нажатию кнопки)

```
UpgradePresenter.OnBuyClicked()
  └─ UpgradesManager.LevelUp(id)
        ├─ CanLevelUp() → проверяет !IsMaxLevel && MoneyStorage.CanSpendMoney(price)
        ├─ MoneyStorage.SpendMoney(price)
        ├─ upgrade.LevelUp()
        │     ├─ _level++
        │     └─ OnUpgrade()   // конкретный класс пишет новое значение в ConverterData
        └─ OnLevelUp?.Invoke(upgrade)   // все UpgradePresenter обновляют UI
```

### Уровни

- `Level` начинается с `1` — это стартовое состояние, применяется при инициализации
- Первая покупка поднимает до `Level = 2`
- При `MaxLevel = 5` доступно **4 покупки**
- Цена первого уровня в `PriceTable` всегда `0` (стартовый уровень бесплатный)
- `IsMaxLevel` возвращает `true` когда `Level == MaxLevel`

### Открытие UI-попапа

`UpgradeTrigger` (MonoBehaviour на триггере) при входе игрока вызывает:
```csharp
popupManager.ShowPopup(PopupName.CONVERTER_UPGRADES);
```
`ConverterUpgradesPopup.OnShow()` итерирует все конфиги из каталога и для каждого создаёт пару `UpgradeView` + `UpgradePresenter`.

---

## API

### `UpgradesManager`

```csharp
// Проверить, можно ли купить апгрейд (не макс. уровень + хватает денег)
bool CanLevelUp(string id)
bool CanLevelUp(Upgrade upgrade)

// Купить апгрейд (бросает Exception если CanLevelUp == false)
void LevelUp(string id)
void LevelUp(Upgrade upgrade)

// Получить апгрейд по id
Upgrade GetUpgrade(string id)

// Получить все апгрейды
Upgrade[] GetAllUpgrades()

// Событие — срабатывает после успешного LevelUp
event Action<Upgrade> OnLevelUp
```

### `Upgrade` (базовый класс)

```csharp
string Id          // уникальный идентификатор (из конфига)
int Level          // текущий уровень (начинается с 1)
int MaxLevel       // максимальный уровень (из конфига)
int NextPrice      // цена следующего уровня
bool IsMaxLevel    // true если Level == MaxLevel
```

### `UpgradeConfig` (базовый класс ScriptableObject)

```csharp
string Id                          // уникальный идентификатор
int MaxLevel                       // максимальный уровень
UpgradePriceTable PriceTable       // таблица цен
UpgradeMetaData Metadata           // иконка, название, описание

int GetNextPrice(int level)        // цена для указанного уровня
float GetStatValue(int level)      // значение параметра на указанном уровне (реализуется в наследнике)
Upgrade Create()                   // создаёт экземпляр Upgrade (реализуется в наследнике)
```

### `UpgradeCatalog`

```csharp
UpgradeConfig[] GetAllUpgrades()           // все конфиги из каталога
UpgradeConfig FindUpgrade(string id)       // найти конфиг по id (бросает Exception если не найден)
```

### `MoneyStorage`

```csharp
bool CanSpendMoney(int amount)
void SpendMoney(int amount)
void EarnMoney(int amount)

event Action<int> OnMoneyChanged
```

---

## Как добавить новый апгрейд

Пример: апгрейд **Speed** — увеличивает скорость конвертера.

### Шаг 1 — Добавить метод в `ConverterData`

Убедитесь, что параметр, который вы хотите улучшать, доступен через `ConverterData`. Если его нет — добавьте сеттер:

```csharp
// ConverterData.cs
public void SetSpeed(float speed) { _speed = speed; }
```

### Шаг 2 — Создать таблицу значений

Если подходят существующие (`CapacityTable` или `TimeConvertationTable`) — используйте их. Если нужна своя формула — создайте по аналогии:

```csharp
// SpeedTable.cs
[Serializable]
public sealed class SpeedTable
{
    [SerializeField] private float _startSpeed = 1f;
    [SerializeField] private float _endSpeed = 5f;
    [ReadOnly] [SerializeField] private float[] _table;

    public float GetSpeed(int level)
    {
        var index = Mathf.Clamp(level - 1, 0, _table.Length - 1);
        return _table[index];
    }

    public void OnValidate(int maxLevel)
    {
        _table = new float[maxLevel];
        for (var i = 0; i < maxLevel; i++)
        {
            var t = (float)i / (maxLevel - 1);
            _table[i] = Mathf.Lerp(_startSpeed, _endSpeed, t);
        }
    }
}
```

### Шаг 3 — Создать класс `Upgrade`

```csharp
// SpeedUpgrade.cs
public sealed class SpeedUpgrade : Upgrade
{
    private readonly SpeedUpgradeConfig _config;
    private ConverterData _converterData;

    public SpeedUpgrade(SpeedUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    [Inject]
    public void Construct(ConverterDataService converterDataService)
    {
        _converterData = converterDataService.ConverterData;
        _converterData.SetSpeed(_config.SpeedTable.GetSpeed(Level));
    }

    protected override void OnUpgrade()
    {
        var speed = _config.SpeedTable.GetSpeed(Level);
        _converterData.SetSpeed(speed);
    }
}
```

> `Construct` вызывается Zenject после создания объекта. Здесь же применяется начальное значение (Level = 1).

### Шаг 4 — Создать класс `UpgradeConfig`

```csharp
// SpeedUpgradeConfig.cs
[CreateAssetMenu(
    fileName = "SpeedUpgradeConfig",
    menuName = "Configs/Upgrade/New SpeedUpgradeConfig"
)]
public sealed class SpeedUpgradeConfig : UpgradeConfig
{
    public SpeedTable SpeedTable;

    public override Upgrade Create()
    {
        return new SpeedUpgrade(this);
    }

    public override float GetStatValue(int level)
    {
        return SpeedTable.GetSpeed(level);
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        SpeedTable.OnValidate(MaxLevel);
    }
}
```

> `OnValidate` вызывается Unity в редакторе при изменении полей — пересчитывает таблицы.

### Шаг 5 — Создать ScriptableObject в Unity Editor

1. В Project window: `ПКМ → Create → Configs/Upgrade → New SpeedUpgradeConfig`
2. Заполнить поля:

| Поле | Значение |
|---|---|
| `Id` | `"speed"` (уникальная строка) |
| `MaxLevel` | Желаемое количество уровней (например, `5`) |
| `PriceTable.BasePrice` | Базовая цена (цены рассчитываются автоматически в OnValidate) |
| `SpeedTable` | Настроить `StartSpeed`, `EndSpeed` |
| `Metadata.Title` | Название апгрейда для UI |
| `Metadata.Description` | Описание для UI |
| `Metadata.Icon` | Иконка (Sprite) |

> Таблицы пересчитываются автоматически при изменении любого поля — не нужно трогать `_table` вручную.

### Шаг 6 — Добавить в `UpgradeCatalog`

1. Найти `UpgradeCatalog.asset` в папке `Assets/_UpgradePractice/Scripts/UpgradesSystem/`
2. Добавить созданный `SpeedUpgradeConfig` в массив `_configs`

Всё. UI создастся автоматически при открытии попапа.

---

## Как добавить апгрейды для новой сущности

Добавление апгрейдов для новой сущности (не конвертера) отличается одним важным шагом: нужно создать `Data`-класс сущности и `DataService`-обёртку, затем забиндить их в Zenject. Апгрейды инжектируют новый сервис вместо `ConverterDataService`.

**Пример: апгрейды игрока** — скорость бега, максимальное HP, урон в ближнем бою.

```
PlayerData              ← хранит параметры игрока
PlayerDataService       ← обёртка для инъекции через Zenject

RunSpeedUpgrade         → инжектит PlayerDataService, пишет в PlayerData.Speed
MaxHpUpgrade            → инжектит PlayerDataService, пишет в PlayerData.MaxHp
MeleeDamageUpgrade      → инжектит PlayerDataService, пишет в PlayerData.MeleeDamage
```

---

### Шаг 1 — Создать `PlayerData`

Аналог `ConverterData` — хранит текущие параметры сущности. Обычный C#-класс.

```csharp
// PlayerUpgrades/Data/PlayerData.cs
public sealed class PlayerData
{
    public float Speed { get; private set; }
    public int MaxHp { get; private set; }
    public float MeleeDamage { get; private set; }

    public PlayerData(float startSpeed, int startMaxHp, float startMeleeDamage)
    {
        Speed = startSpeed;
        MaxHp = startMaxHp;
        MeleeDamage = startMeleeDamage;
    }

    public void SetSpeed(float speed) { Speed = speed; }
    public void SetMaxHp(int maxHp) { MaxHp = maxHp; }
    public void SetMeleeDamage(float meleeDamage) { MeleeDamage = meleeDamage; }
}
```

---

### Шаг 2 — Создать `PlayerDataService`

Обёртка, через которую Zenject доставляет `PlayerData` в апгрейды. Аналог `ConverterDataService`.

```csharp
// PlayerUpgrades/Data/PlayerDataService.cs
public sealed class PlayerDataService
{
    public PlayerData PlayerData { get; }

    public PlayerDataService(PlayerData playerData)
    {
        PlayerData = playerData;
    }
}
```

---

### Шаг 3 — Создать классы апгрейдов

Каждый апгрейд инжектирует `PlayerDataService` и пишет один параметр в `PlayerData`.

**RunSpeedUpgrade** (полный пример):

```csharp
// PlayerUpgrades/RunSpeed/RunSpeedUpgrade.cs
public sealed class RunSpeedUpgrade : Upgrade
{
    private readonly RunSpeedUpgradeConfig _config;
    private PlayerData _playerData;

    public RunSpeedUpgrade(RunSpeedUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    [Inject]
    public void Construct(PlayerDataService playerDataService)
    {
        _playerData = playerDataService.PlayerData;
        _playerData.SetSpeed(_config.SpeedTable.GetSpeed(Level));
    }

    protected override void OnUpgrade()
    {
        _playerData.SetSpeed(_config.SpeedTable.GetSpeed(Level));
    }
}
```

**MaxHpUpgrade** (по аналогии):

```csharp
// PlayerUpgrades/MaxHp/MaxHpUpgrade.cs
public sealed class MaxHpUpgrade : Upgrade
{
    private readonly MaxHpUpgradeConfig _config;
    private PlayerData _playerData;

    public MaxHpUpgrade(MaxHpUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    [Inject]
    public void Construct(PlayerDataService playerDataService)
    {
        _playerData = playerDataService.PlayerData;
        _playerData.SetMaxHp(_config.HpTable.GetHp(Level));
    }

    protected override void OnUpgrade()
    {
        _playerData.SetMaxHp(_config.HpTable.GetHp(Level));
    }
}
```

**MeleeDamageUpgrade** (по аналогии):

```csharp
// PlayerUpgrades/MeleeDamage/MeleeDamageUpgrade.cs
public sealed class MeleeDamageUpgrade : Upgrade
{
    private readonly MeleeDamageUpgradeConfig _config;
    private PlayerData _playerData;

    public MeleeDamageUpgrade(MeleeDamageUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    [Inject]
    public void Construct(PlayerDataService playerDataService)
    {
        _playerData = playerDataService.PlayerData;
        _playerData.SetMeleeDamage(_config.DamageTable.GetDamage(Level));
    }

    protected override void OnUpgrade()
    {
        _playerData.SetMeleeDamage(_config.DamageTable.GetDamage(Level));
    }
}
```

---

### Шаг 4 — Создать классы конфигов

Каждый конфиг хранит свою таблицу значений. Таблицы можно брать готовые (`TimeConvertationTable` для линейного роста, `CapacityTable` для экспоненциального) или создавать свои.

**RunSpeedUpgradeConfig:**

```csharp
// PlayerUpgrades/RunSpeed/RunSpeedUpgradeConfig.cs
[CreateAssetMenu(
    fileName = "RunSpeedUpgradeConfig",
    menuName = "Configs/Upgrade/Player/New RunSpeedUpgradeConfig"
)]
public sealed class RunSpeedUpgradeConfig : UpgradeConfig
{
    public TimeConvertationTable SpeedTable; // линейный рост — подходит для скорости

    public override Upgrade Create() => new RunSpeedUpgrade(this);

    public override float GetStatValue(int level) => SpeedTable.GetTime(level);

    protected override void OnValidate()
    {
        base.OnValidate();
        SpeedTable.OnValidate(MaxLevel);
    }
}
```

**MaxHpUpgradeConfig:**

```csharp
// PlayerUpgrades/MaxHp/MaxHpUpgradeConfig.cs
[CreateAssetMenu(
    fileName = "MaxHpUpgradeConfig",
    menuName = "Configs/Upgrade/Player/New MaxHpUpgradeConfig"
)]
public sealed class MaxHpUpgradeConfig : UpgradeConfig
{
    public CapacityTable HpTable; // экспоненциальный рост — большой прирост на старте

    public override Upgrade Create() => new MaxHpUpgrade(this);

    public override float GetStatValue(int level) => HpTable.GetCapacity(level);

    protected override void OnValidate()
    {
        base.OnValidate();
        HpTable.OnValidate(MaxLevel);
    }
}
```

**MeleeDamageUpgradeConfig:**

```csharp
// PlayerUpgrades/MeleeDamage/MeleeDamageUpgradeConfig.cs
[CreateAssetMenu(
    fileName = "MeleeDamageUpgradeConfig",
    menuName = "Configs/Upgrade/Player/New MeleeDamageUpgradeConfig"
)]
public sealed class MeleeDamageUpgradeConfig : UpgradeConfig
{
    public TimeConvertationTable DamageTable; // линейный рост урона

    public override Upgrade Create() => new MeleeDamageUpgrade(this);

    public override float GetStatValue(int level) => DamageTable.GetTime(level);

    protected override void OnValidate()
    {
        base.OnValidate();
        DamageTable.OnValidate(MaxLevel);
    }
}
```

---

### Шаг 5 — Забиндить `PlayerDataService` в `SceneInstaller`

Это единственное изменение в инфраструктуре. Добавить биндинг в `SceneInstaller`:

```csharp
// DI/SceneInstaller.cs
public override void InstallBindings()
{
    _helper = FindObjectOfType<SceneInstallerHelper>();

    BindConverterDataService();
    BindMoneyStorage();
    BindUpgradeManager();
    BindPlayerDataService(); // добавить
}

private void BindPlayerDataService()
{
    var playerData = new PlayerData(
        startSpeed: 5f,
        startMaxHp: 100,
        startMeleeDamage: 10f
    );
    Container.Bind<PlayerDataService>().AsSingle().WithArguments(playerData);
}
```

Стартовые значения можно вынести в `ScriptableObject` (аналог `SceneInstallerHelper`) — по той же схеме что `ResourceExchangeRates` у конвертера.

---

### Шаг 6 — Создать ScriptableObject assets в Unity Editor

1. `ПКМ → Create → Configs/Upgrade/Player → New RunSpeedUpgradeConfig`
2. То же для `MaxHpUpgradeConfig` и `MeleeDamageUpgradeConfig`
3. Заполнить каждый:

| Поле | RunSpeed | MaxHp | MeleeDamage |
|---|---|---|---|
| `Id` | `"player_run_speed"` | `"player_max_hp"` | `"player_melee_damage"` |
| `MaxLevel` | `5` | `5` | `5` |
| `PriceTable.BasePrice` | `100` | `150` | `120` |
| Таблица значений | `SpeedTable` (3→8) | `HpTable` (100→300) | `DamageTable` (10→50) |
| `Metadata.Title` | Скорость бега | Максимальное HP | Урон в ближнем бою |

---

### Шаг 7 — Добавить конфиги в `UpgradeCatalog`

Открыть `UpgradeCatalog.asset` и добавить все три конфига в массив `_configs`. Порядок в массиве = порядок в UI попапе.

> `UpgradeCatalog` может содержать апгрейды для любых сущностей вперемешку — менеджер не разделяет их по принадлежности. Если нужны отдельные попапы для игрока и конвертера — создайте два `UpgradeCatalog.asset` и два `UpgradesManager`.

---

### Итог: что нужно создать

| Файл | Тип | Назначение |
|---|---|---|
| `PlayerData.cs` | plain C# class | Хранит параметры игрока |
| `PlayerDataService.cs` | plain C# class | Обёртка для Zenject |
| `RunSpeedUpgrade.cs` | Upgrade | Логика апгрейда скорости |
| `RunSpeedUpgradeConfig.cs` | UpgradeConfig | Конфиг скорости |
| `MaxHpUpgrade.cs` | Upgrade | Логика апгрейда HP |
| `MaxHpUpgradeConfig.cs` | UpgradeConfig | Конфиг HP |
| `MeleeDamageUpgrade.cs` | Upgrade | Логика апгрейда урона |
| `MeleeDamageUpgradeConfig.cs` | UpgradeConfig | Конфиг урона |
| `RunSpeedUpgradeConfig.asset` | ScriptableObject | Данные в редакторе |
| `MaxHpUpgradeConfig.asset` | ScriptableObject | Данные в редакторе |
| `MeleeDamageUpgradeConfig.asset` | ScriptableObject | Данные в редакторе |

В `SceneInstaller` — один новый метод `BindPlayerDataService()`.
В `UpgradeCatalog` — три новых конфига в массиве.

---

## Биндинг в Zenject

### Схема зависимостей

```
SceneInstaller
  ├─ ConverterDataService  ← singleton, аргумент: new ConverterData(exchangeRates)
  ├─ MoneyStorage          ← singleton
  └─ UpgradesManager       ← singleton, аргумент: UpgradeCatalog (из SceneInstallerHelper)

ConverterUpgradesPopup (MonoBehaviour)
  └─ [Inject] UpgradesManager

Upgrade (plain class, создаётся внутри UpgradesManager)
  └─ [Inject] ConverterDataService
```

### `SceneInstaller`

```csharp
// DI/SceneInstaller.cs
public class SceneInstaller : MonoInstaller<SceneInstaller>
{
    public override void InstallBindings()
    {
        _helper = FindObjectOfType<SceneInstallerHelper>();

        BindConverterDataService();
        BindMoneyStorage();
        BindUpgradeManager();
    }

    private void BindConverterDataService()
    {
        var converterData = new ConverterData(_helper.ResourceExchangeRates);
        Container.Bind<ConverterDataService>().AsSingle().WithArguments(converterData);
    }

    private void BindMoneyStorage()
    {
        Container.Bind<MoneyStorage>().AsSingle();
    }

    private void BindUpgradeManager()
    {
        Container.Bind<UpgradesManager>().AsSingle().WithArguments(_helper.UpgradeCatalog);
    }
}
```

### `SceneInstallerHelper`

MonoBehaviour на объекте сцены. Хранит ссылки на ScriptableObject-ы, которые нельзя получить через Zenject автоматически.

```csharp
// DI/SceneInstallerHelper.cs
// Сериализованные поля в инспекторе:
// _upgradeCatalog      → UpgradeCatalog.asset
// _resourceExchangeRates → List<ResourceExchangeRate>
```

### Инъекция в `Upgrade`

Апгрейды — обычные C#-классы, Zenject создаёт их не сам. `UpgradesManager` создаёт их через `config.Create()`, затем вручную инжектирует зависимости:

```csharp
// UpgradeManager.cs
var upgrade = config.Create();
_container.Inject(upgrade);   // Zenject вызовет метод с [Inject]
```

Поэтому в каждом конкретном классе апгрейда — метод с атрибутом `[Inject]`:

```csharp
[Inject]
public void Construct(ConverterDataService converterDataService)
{
    _converterData = converterDataService.ConverterData;
    // применяем начальное значение
}
```

### Инъекция в MonoBehaviour (попап)

```csharp
// ConverterUpgradesPopup.cs
[Inject]
public void Construct(UpgradesManager upgradesManager)
{
    _upgradesManager = upgradesManager;
    _upgradeCatalog = upgradesManager.UpgradeCatalog;
}
```

Zenject сам находит MonoBehaviour на объектах сцены и вызывает `[Inject]`-методы при запуске.

### Что добавить в Zenject при новом апгрейде

**Ничего.** Новый апгрейд не требует изменений в инсталлере. Достаточно:
1. Добавить конфиг в `UpgradeCatalog`
2. Убедиться, что все зависимости апгрейда уже забиндены (например, `ConverterDataService` уже есть)

Если новый апгрейд требует новую зависимость — добавить биндинг в `SceneInstaller`.
