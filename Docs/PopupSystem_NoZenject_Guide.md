# Popup System (No Zenject + Service Locator) — Руководство

## Архитектура

Система состоит из 5 слоёв:

```
ServiceLocator          — хранит зависимости (PlayerLevel и др.)
PopupCatalog            — ScriptableObject, реестр всех попапов
PopupManagerNoZenject   — управляет показом/скрытием попапов
PopupPresenter          — базовый класс, отвечает только за lifecycle попапа (Show/Hide/анимации)
ViewPresenter           — отдельный MonoBehaviour для каждой вью: подписки на модель, refresh
PassiveView             — чистая пассивная вью, не знает ни о модели, ни о презентере
```

**Правила:**
- У каждой вьюшки есть свой презентер (`ViewPresenter`)
- `PopupPresenter` делегирует управление данными своему `ViewPresenter`, сам отвечает только за lifecycle
- В один момент времени открыт только один попап
- Попапы создаются лениво — при первом вызове `Show`
- Если `Cached = true` в каталоге — экземпляр переиспользуется, иначе уничтожается при скрытии

---

## Структура сцены

```
[Scene]
 ├── ServiceLocatorInstaller      (регистрирует зависимости в Awake)
 ├── PopupManager                 (компонент PopupManagerNoZenject)
 │    ├── Pool                    (дочерний GO — хранилище скрытых попапов, неактивен)
 │    └── Viewport                (дочерний GO — сюда помещается активный попап)
 └── PopupTesterNoZenject         (опционально, для тестирования в Play Mode)
```

### Поля PopupManagerNoZenject в инспекторе

| Поле | Что назначить |
|---|---|
| `_pool` | Transform дочернего объекта Pool |
| `_viewport` | Transform дочернего объекта Viewport |
| `_catalog` | `PopupCatalog.asset` |

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

Отдельный MonoBehaviour, который знает о вью и модели. Менеджит подписки.

```csharp
public sealed class MyPopupViewPresenter : MonoBehaviour
{
    [SerializeField] private MyPopupView _view;

    private SomeDependency _dependency;

    private void Start()
    {
        _dependency = ServiceLocator.ServiceLocator.Instance.Get<SomeDependency>();
    }

    public void Show()
    {
        if (_dependency == null)
            _dependency = ServiceLocator.ServiceLocator.Instance.Get<SomeDependency>();

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

> **Почему `Start` + null-check в `Show`:**  
> `Start` вызывается раньше первого `Show` при нормальном сценарии.  
> Null-check — страховка на случай если попап закэширован и переиспользуется между сценами.

### Шаг 3. Создать PopupPresenter

Тонкая обёртка — только lifecycle: делегирует Show/Hide своему ViewPresenter, управляет анимацией.

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

На одном GO три компонента:

```
[MyPopup GO]
 ├── MyPopupPresenter     → _presenter: [ссылка на MyPopupViewPresenter]
 │                        → _popupView: [ссылка на PopupView, опционально]
 ├── MyPopupViewPresenter → _view: [ссылка на MyPopupView]
 └── MyPopupView          → (UI-поля: TextMeshPro, кнопки и т.д.)
```

> Все три компонента можно держать на одном GO или разнести по дочерним — главное назначить ссылки в инспекторе.

### Шаг 5. Добавить значение в enum

```csharp
// PopupType.cs — Modules.Popups
public enum PopupType
{
    PlayerLevel,
    PlayerStats,
    MyPopup      // ← добавить новое значение
}
```

### Шаг 6. Зарегистрировать в PopupCatalog

Открыть `PopupCatalog.asset` → в массиве `_presenters` добавить новый элемент:

| Поле | Значение |
|---|---|
| `Type` | `MyPopup` |
| `Cached` | `true` — переиспользовать / `false` — пересоздавать каждый раз |
| `Prefab` | перетащить `MyPopup.prefab` |

---

## Управление попапами из кода

### Показать по enum (основной способ)

```csharp
_popupManager.Show(PopupType.MyPopup);
```

### Показать по типу (type-safe, без enum)

```csharp
_popupManager.Show<MyPopupPresenter>();
```

### Показать с аргументами

Если попапу нужны данные при открытии — используем `IPopupArgs`.

**1. Объявить struct с данными:**
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
        _presenter.Show(args); // передаём args в ViewPresenter если нужно
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

### Подписаться на события

```csharp
_popupManager.OnShow += presenter => Debug.Log($"Открыт: {presenter.GetType().Name}");
_popupManager.OnHide += presenter => Debug.Log($"Закрыт: {presenter.GetType().Name}");
```

---

## Анимации (PopupView)

`PopupView` — компонент на корневом объекте префаба попапа. Реализует плавный показ/скрытие через DOTween.

### Настройка в префабе

1. Добавить компонент `PopupView` на корневой GO префаба
2. Назначить `_animationRoot` (обычно `this.transform` — сбрасывается автоматически через `Reset`)
3. В инспекторе `PopupPresenter`-а назначить `_popupView`

### Поведение анимаций

| Метод | Анимация |
|---|---|
| `AnimateShow()` | Scale от 0 до 1, `Ease.OutBack` — "упругое" появление |
| `AnimateHide(callback)` | Scale от 1 до 0, `Ease.InBack`, затем вызывает callback |
| `Show()` | Мгновенно `SetActive(true)` без анимации |
| `Hide()` | Мгновенно `SetActive(false)` без анимации |

### Без анимации

Если `_popupView` не назначен — попап открывается и закрывается мгновенно.  
Никаких изменений в коде не требуется.

### Как работает Hide с анимацией

Менеджер не перемещает попап в pool сразу — он передаёт колбэк:

```
Manager.Hide()
 └── popup.Hide(callback: MoveToPool)
      └── PopupView.AnimateHide(() => MoveToPool())
           └── [0.35s анимация]
                └── popup.transform.SetParent(_pool)
```

`OnHide` event и `_current = null` срабатывают сразу — до завершения анимации.

---

## Кэширование

| `Cached` | Поведение | Когда использовать |
|---|---|---|
| `true` | Экземпляр создаётся один раз, переиспользуется при повторных `Show` | Тяжёлые попапы, часто открываемые |
| `false` | Каждый `Show` — новый `Instantiate`, после `Hide` уничтожается | Одноразовые попапы, диалоги подтверждения |

---

## Регистрация зависимостей в ServiceLocator

Все зависимости регистрируются в `ServiceLocatorInstaller` при старте сцены — **до** первого открытия попапа.

```csharp
public sealed class ServiceLocatorInstaller : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.Instance.Register<PlayerLevel>(new PlayerLevel());
        ServiceLocator.Instance.Register<SomeDependency>(new SomeDependency());
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterAll();
    }
}
```

> **Порядок гарантирован:** `ServiceLocatorInstaller.Awake` выполняется при старте сцены.  
> Попап создаётся только при первом `Show` — то есть всегда позже.

---

## Тестирование через PopupTesterNoZenject

Компонент для тестирования в Play Mode без написания кода.

**Поля в инспекторе:**

| Поле | Что назначить |
|---|---|
| `_popupManager` | ссылка на `PopupManagerNoZenject` в сцене |
| `_popupType` | выбрать нужный попап из выпадающего списка enum |

**Кнопки (Odin Inspector):**
- **Show** — открыть выбранный попап
- **Hide** — закрыть текущий попап

---

## Структура файлов

```
PlayerPopupMonobeh/
 ├── PlayerLevelPopupPresenter.cs   — popup presenter для уровня (lifecycle only)
 ├── PlayerLevelPresenter.cs        — view presenter для уровня (данные + подписки)
 ├── PlayerLevelPassiveView.cs      — пассивная вью уровня

 └── PopupNoZenjectBase/
      ├── PopupManagerNoZenject.cs      — менеджер попапов
      ├── PopupTesterNoZenject.cs       — тестер для Play Mode
      ├── PlayerStatsPopupPresenter.cs  — popup presenter для статистики (lifecycle only)
      ├── PlayerStatsPresenter.cs       — view presenter для статистики (данные)
      └── PlayerStatsPassiveView.cs     — пассивная вью статистики

PopupZenjectBase/Scripts/           — общие базовые классы (Modules.Popups)
 ├── PopupType.cs                   — enum всех типов попапов
 ├── IPopupArgs.cs                  — интерфейс аргументов
 ├── Catalog/
 │    ├── PopupCatalog.cs           — ScriptableObject каталог
 │    └── PopupInfo.cs              — запись каталога (Type + Cached + Prefab)
 ├── Presenter/
 │    ├── PopupPresenter.cs         — абстрактный базовый класс + Hide(Action)
 │    └── PopupPresenter`1.cs       — generic-версия с типизированными args
 └── View/
      └── PopupView.cs              — вью с анимацией Show/Hide (DOTween)
```
