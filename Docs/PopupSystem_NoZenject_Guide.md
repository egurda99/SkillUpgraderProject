# Popup System (No Zenject + Service Locator) — Руководство

## Архитектура

Система состоит из 4 слоёв:

```
ServiceLocator          — хранит зависимости (PlayerLevel и др.)
PopupCatalog            — ScriptableObject, реестр всех попапов
PopupManagerNoZenject   — управляет показом/скрытием попапов
PopupPresenter          — базовый класс каждого конкретного попапа
```

**Правила:**
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

### Шаг 1. Создать View

```csharp
public sealed class MyPopupView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;

    public void SetTitle(string title) => _titleText.text = title;
}
```

### Шаг 2. Создать Presenter

```csharp
public sealed class MyPopupPresenter : PopupPresenter
{
    [SerializeField] private MyPopupView _view;
    [SerializeField] private PopupView _popupView; // опционально — для анимаций

    private SomeDependency _dependency;

    private void Awake()
    {
        // Зависимости берём из ServiceLocator — Awake вызывается один раз при первом Show
        _dependency = ServiceLocator.ServiceLocator.Instance.Get<SomeDependency>();
    }

    public override void Show(IPopupArgs args)
    {
        _view.SetTitle("Hello");

        if (_popupView != null)
            _popupView.AnimateShow();
        else
            _view.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        if (_popupView != null)
            _popupView.Hide();
        else
            _view.gameObject.SetActive(false);
    }

    // Переопределяем для анимированного скрытия
    public override void Hide(Action onComplete)
    {
        if (_popupView != null)
            _popupView.AnimateHide(() => onComplete?.Invoke());
        else
        {
            _view.gameObject.SetActive(false);
            onComplete?.Invoke();
        }
    }
}
```

> **Важно:** зависимости получаем в `Awake`, не в `Show`.  
> `Awake` вызывается один раз — при первом открытии попапа.  
> `Show` / `Hide` могут вызываться многократно.

### Шаг 3. Создать префаб

1. Создать новый GameObject в сцене
2. Добавить компоненты `MyPopupPresenter`, `MyPopupView`, `PopupView` (если нужна анимация)
3. Назначить `_view` в инспекторе
4. Если нужна анимация — назначить `_popupView` и настроить `_animationRoot`
5. Сохранить как префаб

### Шаг 4. Добавить значение в enum

```csharp
// PopupType.cs — Modules.Popups
public enum PopupType
{
    PlayerLevel,
    PlayerStats,
    MyPopup      // ← добавить новое значение
}
```

### Шаг 5. Зарегистрировать в PopupCatalog

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

**2. Presenter наследуется от `PopupPresenter<T>`:**
```csharp
public sealed class MyPopupPresenter : PopupPresenter<MyPopupArgs>
{
    public override void Show(MyPopupArgs args)
    {
        _view.SetTitle(args.Title);
        _view.SetValue(args.Value);
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
3. В инспекторе презентера назначить `_popupView`

### Поведение анимаций

| Метод | Анимация |
|---|---|
| `AnimateShow()` | Scale от 0 до 1, `Ease.OutBack` — "упругое" появление |
| `AnimateHide(callback)` | Scale от 1 до 0, `Ease.InBack`, затем вызывает callback |
| `Show()` | Мгновенно `SetActive(true)` без анимации |
| `Hide()` | Мгновенно `SetActive(false)` без анимации |

### Без анимации

Если `_popupView` не назначен в инспекторе — попап открывается и закрывается мгновенно.  
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
PopupNoZenjectBase/
 ├── PopupManagerNoZenject.cs       — менеджер попапов
 ├── PopupTesterNoZenject.cs        — тестер для Play Mode
 ├── PlayerLevelPopupPresenter.cs   — пример попапа с уровнем игрока
 ├── PlayerStatsPopupPresenter.cs   — пример попапа со статистикой
 ├── PlayerStatsView.cs             — вью для PlayerStats
 └── PopupCatalog.asset             — реестр попапов (ScriptableObject)

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
