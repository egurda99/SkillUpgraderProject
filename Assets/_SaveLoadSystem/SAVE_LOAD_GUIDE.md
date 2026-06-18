# Save/Load System — Руководство по использованию

## Архитектура системы

```
GameRepository          — хранит данные в памяти (Dictionary<string, string>)
    ↓ сериализует через Newtonsoft.Json
IGameStateSaver         — записывает/читает с диска (AES-шифрование)
    ↓
SaveLoadManager         — оркестрирует все ISaveLoader
    ↓ вызывает
ISaveLoader[]           — каждый отвечает за свой кусок данных
    ↑ реализует
SaveLoader<TService, TData>  — базовый класс с логикой ключа
```

---

## Шаг 1 — Глобальная инициализация (ProjectContext)

`SaveSystemInstaller` вешается на `ProjectContext` и живёт всё время работы приложения.

```csharp
// SaveSystemInstaller.cs — уже готов, трогать не нужно
public sealed class SaveSystemInstaller : MonoInstaller<SaveSystemInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameRepository>().AsSingle();
        Container.Bind<SaveLoadManager>().AsSingle().NonLazy();
    }
}
```

**В Unity:** `ProjectContext → MonoInstallers → добавить SaveSystemInstaller`

---

## Шаг 2 — Инициализация на сцене (SceneContext)

`SceneContainerUpdater` на старте сцены передаёт свой `DiContainer` в `SaveLoadManager`.
Это нужно потому что `SaveLoader`-ы биндятся на уровне сцены, а `SaveLoadManager` живёт глобально.

```csharp
// SceneContainerUpdater.cs — уже готов, вешается как MonoBehaviour на сцену
public sealed class SceneContainerUpdater : MonoBehaviour
{
    [Inject]
    public void Construct(SaveLoadManager saveLoadManager, DiContainer container)
    {
        _saveLoadManager = saveLoadManager;
        _container = container;
    }

    private void Start()
    {
        _saveLoadManager.InitOnNewScene(_container); // регистрирует все ISaveLoader со сцены
    }
}
```

**В Unity:** добавить `SceneContainerUpdater` как MonoBehaviour в `SceneContext`.

---

## Шаг 3 — Бинд SaveLoader-а на сцене

Каждый `SaveLoader` биндится через свой `MonoInstaller` в `SceneContext`.
Ключевой момент — `BindInterfacesAndSelfTo`, чтобы `SaveLoadManager` мог найти его через `ISaveLoader`.

```csharp
public sealed class MoneySaveLoaderInstaller : MonoInstaller<MoneySaveLoaderInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MoneySaveLoader>().AsSingle().NonLazy();
    }
}
```

**В Unity:** `SceneContext → MonoInstallers → добавить MoneySaveLoaderInstaller`

---

## Шаг 4 — Вызов Save и Load

```csharp
public class GameBootstrap : MonoBehaviour
{
    [Inject] private SaveLoadManager _saveLoadManager;

    private void Start()
    {
        _saveLoadManager.Load(); // читает файл → десериализует → вызывает SetupData у каждого loader-а
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            _saveLoadManager.Save(); // вызывает ConvertToData → сериализует → шифрует → пишет файл
    }
}
```

**Порядок вызовов внутри `Load()`:**
1. `GameRepository.LoadState()` — читает `gamestate.sav` с диска, расшифровывает, заполняет словарь
2. Для каждого `ISaveLoader` → `LoadGame(repository, context)`
3. Внутри `LoadGame`: `repository.TryGetData(Key, out TData data)` → `SetupData(service, data)`
4. Если данных нет → `SetupDefaultData(service)`

**Порядок вызовов внутри `Save()`:**
1. Для каждого `ISaveLoader` → `SaveGame(repository, context)`
2. Внутри `SaveGame`: `ConvertToData(service)` → `repository.SetData(Key, data)`
3. `GameRepository.SaveState()` — сериализует словарь → шифрует → пишет файл

---

## Шаг 5 — Добавление нового SaveLoader

Допустим нужно сохранять опыт игрока (`ExperienceService`).

### 5.1 — Data-класс

```csharp
// ExperienceData.cs
[Serializable]
public sealed class ExperienceData
{
    public int Level;
    public float CurrentXp;
}
```

### 5.2 — SaveLoader

```csharp
// ExperienceSaveLoader.cs
[Serializable]
public sealed class ExperienceSaveLoader : SaveLoader<ExperienceService, ExperienceData>
{
    protected override string Key => "experience"; // явный ключ — не зависит от имени класса

    protected override ExperienceData ConvertToData(ExperienceService service)
    {
        return new ExperienceData
        {
            Level = service.Level,
            CurrentXp = service.CurrentXp
        };
    }

    protected override void SetupData(ExperienceService service, ExperienceData data)
    {
        service.Setup(data.Level, data.CurrentXp);
    }

    protected override void SetupDefaultData(ExperienceService service)
    {
        service.Setup(level: 1, currentXp: 0f);
    }
}
```

### 5.3 — Installer

```csharp
// ExperienceSaveLoaderInstaller.cs
public sealed class ExperienceSaveLoaderInstaller : MonoInstaller<ExperienceSaveLoaderInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ExperienceSaveLoader>().AsSingle().NonLazy();
    }
}
```

### 5.4 — Подключение в Unity

`SceneContext → MonoInstallers → добавить ExperienceSaveLoaderInstaller`

Больше ничего. `SaveLoadManager` подхватит новый loader автоматически через `GetServices<ISaveLoader>()`.

---

## Схема потока данных

```
[Диск: gamestate.sav]
        ↕  AES шифрование
[EncryptionFileGameStateSaver]
        ↕  Dictionary<string, string>
[GameRepository]
        ↕  TryGetData("experience", out ExperienceData)
            SetData("experience", data)
[ExperienceSaveLoader]
        ↕  ConvertToData / SetupData
[ExperienceService]
```

---

## Правила именования ключей

| Правило | Почему |
|---|---|
| Всегда переопределять `Key` в конкретном loader-е | Дефолт `typeof(TData).Name` — сломается при переименовании класса |
| Ключ в нижнем регистре, без пробелов | `"experience"`, `"money"`, `"units_v2"` |
| При смене схемы данных — новый ключ (`"units_v2"`) | Старый сейв не будет десериализован в новую структуру — явная миграция |

---

## Где что лежит

```
Assets/_SaveLoadSystem/Scripts/SaveSystem/
├── Base/
│   ├── IGameRepository.cs          — интерфейс хранилища
│   ├── GameRepository.cs           — реализация (Dictionary + JSON)
│   ├── ISaveLoader.cs              — интерфейс одного loader-а
│   ├── SaveLoader.cs               — базовый абстрактный класс
│   ├── SaveLoadManager.cs          — оркестратор (Save/Load)
│   └── GameStateSavers/
│       └── EncryptionFileGameStateSaver.cs  — запись на диск с AES
├── Money/
│   ├── MoneyData.cs
│   ├── MoneySaveLoader.cs
│   └── Installer/MoneySaveLoaderInstaller.cs
└── DI/
    ├── GlobalInstallers/SaveSystemInstaller.cs   — ProjectContext
    └── SceneContainerUpdater.cs                  — SceneContext
```
