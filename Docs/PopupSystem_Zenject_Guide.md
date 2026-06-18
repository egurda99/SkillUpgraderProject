# Popup System (Zenject DI) — Руководство

## Архитектура

Система состоит из 5 слоёв — тех же, что в NoZenject-версии, но зависимости предоставляются через Zenject DI-контейнер:

```
Zenject Container       — хранит зависимости (PlayerLevel, PopupCatalog, PopupManagerZenject)
PopupCatalog            — ScriptableObject, реестр всех попапов
PopupManagerZenject     — управляет показом/скрытием попапов
PopupPresenter          — базовый класс, отвечает только за lifecycle (Show/Hide/анимации)
ViewPresenter           — отдельный MonoBehaviour для каждой вью: подписки на модель, refresh
PassiveView             — чистая пассивная вью, не знает ни о модели, ни о презентере
```

**Ключевое отличие от NoZenject:**  
Зависимости получаются не через `ServiceLocator.Instance.Get<T>()`, а через `[Inject] public void Construct(T dep)`.  
Zenject автоматически инжектирует в каждый `MonoBehaviour` на созданном префабе — при условии, что попап создаётся через `IInstantiator.InstantiatePrefabForComponent`.

**Правила (те же, что в NoZenject):**
- У каждой вьюшки есть свой `ViewPresenter`
- `PopupPresenter` делегирует управление данными своему `ViewPresenter`
- В один момент времени открыт только один попап
- Попапы создаются лениво — при первом вызове `Show`
- Если `Cached = true` в каталоге — экземпляр переиспользуется

---

## Структура сцены

```
[Scene]
 ├── SceneContext                 (Zenject — точка входа контейнера)
 │    └── MonoInstallers: [PopupZenjectInstaller]
 ├── PopupZenjectInstaller        (MonoInstaller, регистрирует зависимости)
 ├── PopupManager                 (компонент PopupManagerZenject)
 │    ├── Pool                    (дочерний GO — хранилище скрытых попапов, неактивен)
 │    └── Viewport                (дочерний GO — сюда помещается активный попап)
 └── PopupTesterZenject           (опционально, для тестирования в Play Mode)
```

### Поля PopupZenjectInstaller в инспекторе

| Поле | Что назначить |
|---|---|
| `_catalog` | `PopupCatalog.asset` |
| `_popupManager` | ссылка на GO с компонентом `PopupManagerZenject` |

### Поля PopupManagerZenject в инспекторе

| Поле | Что назначить |
|---|---|
| `_pool` | Transform дочернего объекта Pool |
| `_viewport` | Transform дочернего объекта Viewport |

> `IInstantiator` и `PopupCatalog` инжектируются Zenject-ом автоматически — в инспекторе не назначать.

---

## Как добавить новый попап — пошагово

### Шаг 1. Создать PassiveView

```csharp
public sealed class MyPopupView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;

    public event Action ButtonClicked;

    public void SetTitle(string title) => _titleText.text = title;
}
```

### Шаг 2. Создать ViewPresenter

Отдельный MonoBehaviour. Зависимости от модели получает через `[Inject] Construct()`.

```csharp
public sealed class MyPopupViewPresenter : MonoBehaviour
{
    [SerializeField] private MyPopupView _view;

    private SomeDependency _dependency;

    [Inject]
    public void Construct(SomeDependency dependency)
    {
        _dependency = dependency;
    }

    public void Show()
    {
        _view.ButtonClicked += OnButtonClicked;
        _view.SetTitle(_dependency.GetTitle());
    }

    public void Hide()
    {
        _view.ButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked() => _dependency.DoSomething();
}
```

> **Почему нет null-check в `Show`:**  
> `Construct` гарантированно вызывается Zenject-ом до любого Unity-lifecycle метода.  
> В отличие от ServiceLocator-версии, проверка не нужна.

### Шаг 3. Создать PopupPresenter

Тонкая обёртка — только lifecycle. Сам ничего не знает о модели.

```csharp
public sealed class MyPopupPresenter : PopupPresenter
{
    [SerializeField] private MyPopupViewPresenter _presenter;
    [SerializeField] private PopupView _popupView; // опционально — для анимаций

    public override void Show(IPopupArgs args)
    {
        _presenter.Show();

        if (_popupView != null)
            _popupView.AnimateShow();
    }

    public override void Hide()
    {
        _presenter.Hide();
        _popupView?.Hide();
    }

    public override void Hide(Action onComplete)
    {
        _presenter.Hide();

        if (_popupView != null)
            _popupView.AnimateHide(() => onComplete?.Invoke());
        else
            onComplete?.Invoke();
    }
}
```

### Шаг 4. Создать префаб

На одном GO — три компонента:

```
[MyPopup GO]
 ├── MyPopupPresenter      → _presenter: [ссылка на MyPopupViewPresenter]
 │                         → _popupView: [ссылка на PopupView, опционально]
 ├── MyPopupViewPresenter  → _view: [ссылка на MyPopupView]
 └── MyPopupView           → (UI-поля: TextMeshPro, кнопки и т.д.)
```

> Zenject инжектирует `Construct()` во все `MonoBehaviour` на префабе при `InstantiatePrefabForComponent` — это происходит автоматически, никаких ручных вызовов не нужно.

### Шаг 5. Добавить значение в enum

```csharp
// PopupType.cs — Modules.Popups
public enum PopupType
{
    PlayerLevel,
    PlayerStats,
    Greeting,
    MyPopup   // ← добавить
}
```

### Шаг 6. Зарегистрировать в PopupCatalog

Открыть `PopupCatalog.asset` → в массиве `_presenters` добавить новый элемент:

| Поле | Значение |
|---|---|
| `Type` | `MyPopup` |
| `Cached` | `true` — переиспользовать / `false` — пересоздавать каждый раз |
| `Prefab` | перетащить `MyPopup.prefab` |

### Шаг 7. Зарегистрировать зависимость в инсталлере (если нужно)

Если `MyPopupViewPresenter.Construct()` требует новый тип — добавить бинд в `PopupZenjectInstaller`:

```csharp
public override void InstallBindings()
{
    Container.Bind<PlayerLevel>().AsSingle();
    Container.Bind<SomeDependency>().AsSingle(); // ← добавить
    Container.Bind<PopupCatalog>().FromInstance(_catalog).AsSingle();
    Container.Bind<PopupManagerZenject>().FromInstance(_popupManager).AsSingle();
}
```

---

## Управление попапами из кода

### Показать по enum

```csharp
_popupManager.Show(PopupType.MyPopup);
```

### Показать по типу (type-safe)

```csharp
_popupManager.Show<MyPopupPresenter>();
```

### Показать с аргументами

**1. Объявить struct:**
```csharp
public struct MyPopupArgs : IPopupArgs
{
    public string Title;
    public int Value;
}
```

**2. PopupPresenter наследуется от `PopupPresenter<T>`:**
```csharp
public sealed class MyPopupPresenter : PopupPresenter<MyPopupArgs>
{
    [SerializeField] private MyPopupViewPresenter _presenter;

    public override void Show(MyPopupArgs args)
    {
        _presenter.Show(args.Title);
        // ...
    }
}
```

**3. Вызов:**
```csharp
_popupManager.Show<MyPopupPresenter, MyPopupArgs>(new MyPopupArgs
{
    Title = "Info",
    Value = 42
});
```

### Скрыть текущий попап

```csharp
_popupManager.Hide();
```

### Проверить, открыт ли попап

```csharp
if (_popupManager.IsShown<MyPopupPresenter>())
{
    // попап сейчас открыт
}
```

---

## Анимации (PopupView)

Идентично NoZenject-версии.

| Метод | Анимация |
|---|---|
| `AnimateShow()` | Scale от 0 до 1, `Ease.OutBack` |
| `AnimateHide(callback)` | Scale от 1 до 0, `Ease.InBack`, затем callback |
| `Show()` | Мгновенно `SetActive(true)` |
| `Hide()` | Мгновенно `SetActive(false)` |

Если `_popupView` не назначен — попап открывается мгновенно, изменений в коде не нужно.

---

## Тестирование через PopupTesterZenject

`PopupManagerZenject` приходит через `[Inject]` — поле в инспекторе назначать не нужно.  
Достаточно положить `PopupTesterZenject` на любой GO в сцене под `SceneContext`.

**Поля в инспекторе:**

| Поле | Что назначить |
|---|---|
| `_popupType` | выбрать попап из выпадающего списка enum |

**Кнопки (Odin Inspector):**
- **Show** — открыть выбранный попап
- **Hide** — закрыть текущий попап

---

## Сравнение с NoZenject-версией

| | NoZenject | Zenject |
|---|---|---|
| Получение зависимости | `ServiceLocator.Instance.Get<T>()` в `Start` + null-check в `Show` | `[Inject] Construct(T dep)` |
| Создание попапа | `Instantiate(prefab)` | `IInstantiator.InstantiatePrefabForComponent<T>(prefab)` |
| Регистрация зависимостей | `ServiceLocatorInstaller.Awake` | `PopupZenjectInstaller.InstallBindings` |
| Тестер: получение менеджера | `[SerializeField]` в инспекторе | `[Inject]` автоматически |
| Точка входа в сцене | нет | `SceneContext` |

---

## Структура файлов

```
PopupZenjectBase/
 ├── PlayerLevelPopupPresenterZenject.cs — popup presenter для уровня (lifecycle only)
 ├── PlayerPresenter.cs                  — view presenter для уровня (данные + подписки)
 ├── PlayerStatsPopupPresenterZenject.cs — popup presenter для статистики (lifecycle only)
 ├── PlayerStatsPresenterZenject.cs      — view presenter для статистики (данные)
 ├── GreetingPopupPresenterZenject.cs    — popup presenter для приветствия (lifecycle only)
 ├── GreetingPresenterZenject.cs         — view presenter для приветствия
 ├── GreetingPopupView.cs                — пассивная вью приветствия
 ├── PopupZenjectInstaller.cs            — MonoInstaller, регистрирует зависимости
 └── PopupTesterZenject.cs              — тестер для Play Mode

PopupZenjectBase/Scripts/               — общие базовые классы (Modules.Popups)
 ├── PopupType.cs                        — enum всех типов попапов
 ├── IPopupArgs.cs                       — интерфейс аргументов
 ├── PopupManagerZenject.cs             — менеджер попапов
 ├── Catalog/
 │    ├── PopupCatalog.cs               — ScriptableObject каталог
 │    └── PopupInfo.cs                  — запись каталога (Type + Cached + Prefab)
 ├── Presenter/
 │    ├── PopupPresenter.cs             — абстрактный базовый класс + Hide(Action)
 │    └── PopupPresenter`1.cs           — generic-версия с типизированными args
 └── View/
      └── PopupView.cs                  — вью с анимацией Show/Hide (DOTween)
```
