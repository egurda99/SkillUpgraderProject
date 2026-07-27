# Logging (`com.vavilichev.logging`) — гайд по использованию

Лёгкий логгер для Unity: статический фасад `Log` + переиспользуемые `Logger` с тегами (каналами). Пишет в стандартную Unity-консоль (`Debug.Log/LogWarning/LogError`). Без DI/Zenject, без файлового sink.

## Быстрый старт

Глобальный логгер с тегом `LOGGER` доступен сразу, без какой-либо настройки:

```csharp
using Vavilichev.Tools.Logging;

Log.Info("Player spawned");
Log.Warning("Low ammo");
Log.Error("Save failed");
```

В консоли это даст `[LOGGER] Player spawned` и т.д.

## Свой логгер с тегом

Для конкретной системы/фичи удобнее завести собственный `Logger` с понятным тегом, а не пользоваться общим `Log`:

```csharp
public sealed class GameCycleManager : MonoBehaviour
{
    private readonly Logger _logger = new Logger("GAME_CYCLE");

    public void StartGame()
    {
        _logger.Info("Game started");
    }
}
```

Такой логгер работает сразу и без регистрации в `LoggingSettings` — по умолчанию он включён (`_isEnabled = true`). Настройки `LoggingSettings` применяются к нему, только если он зарегистрирован как именованный канал (см. ниже) — тогда движок сам вызывает `ApplySettings` при старте игры.

## Уровни логирования

Всего три уровня, без `Debug`/`Trace`/`Fatal`:

| Метод | Поведение в билдах |
|---|---|
| `Info` / `Warning` | Вызовы **вырезаются компилятором** (`[Conditional]`) в билде, если не задан `UNITY_EDITOR`, `DEVELOPMENT_BUILD` или scripting define `LOGS_ENABLED`. В релизном билде по умолчанию их не будет вообще — это не runtime-проверка, а стирание самого вызова на этапе компиляции. Мьютятся по каналу независимо друг от друга (см. "Именованные каналы"). |
| `Error` | Компилируется и логируется **всегда**, независимо от билда и мьюта канала, плюс бросает статический `Logger.OnError` (см. ниже). |

### Когда использовать какой уровень

- **`Info`** — обычные, ожидаемые события штатного потока программы: старт игры, смена состояния, загрузка уровня. То, что происходит каждый раз в нормальной работе и не требует внимания, если всё идёт по плану.
- **`Warning`** — событие, которое выбивается из обычного потока программы: неожиданные/невалидные данные, сработавший fallback, состояние, которое не должно было наступить, но игра может продолжать работать. Если код "восстановился сам" после странной ситуации — это `Warning`, а не `Info`.
- **`Error`** — что-то сломалось и достойно попадания в отчёт вне зависимости от билда и настроек канала (см. `Logger.OnError`).

## Включение Info/Warning в релизных билдах

Открыть ассет настроек: меню `Tools/Vavilichev/Logging/Settings` (или файл `Assets/Tools/Logging/Resources/LoggingSettings.asset`). В инспекторе — чекбокс **"Enable Release Build Logs"**. Он проставляет define `LOGS_ENABLED` сразу для всех build target-ов проекта (Standalone, Android, iOS, WebGL и т.д.). Снятие чекбокса убирает define везде.

## Именованные каналы

Каналы позволяют централизованно мьютить/анмьютить логи конкретной системы через инспектор, без пересборки кода, и дают строготипизированный доступ вида `Log.<Имя>.Info(...)`.

1. Открыть `LoggingSettings.asset` (`Tools/Vavilichev/Logging/Settings`).
2. В массиве `Channels` добавить элемент: `PropertyName` (валидный C#-идентификатор, например `Player`), `Tag` (префикс в консоли, например `PLAYER`), `MuteInfo` и `MuteWarning` — раздельные чекбоксы для уровней `Info` и `Warning` (оба по умолчанию `true` — канал замьючен на обоих уровнях, явно снимите галочку для нужного уровня). `Error` этими флагами не управляется — он всегда включён. `Color` — цвет, в который будет окрашено сообщение канала в консоли (по умолчанию белый, то есть без окраски).
3. Нажать **"Regenerate Log API"** в инспекторе (или меню `Tools/Vavilichev/Logging/Regenerate Log File`). Это перегенерирует `Log.Generated.cs`, добавив `public static readonly Logger Player = new Logger("PLAYER");`.
4. Использовать: `Log.Player.Info("...")`.

`Color` применяется ко всем трём уровням (`Info`/`Warning`/`Error`) через rich-text тег `<color=#RRGGBBAA>` — сообщение целиком, включая `[TAG]`, красится в выбранный цвет. Как и `MuteInfo`/`MuteWarning`, цвет применяется только к логгерам, зарегистрированным как именованный канал (`Log.Generated.cs`); логгеры, созданные вручную через `new Logger("TAG")`, всегда выводятся без окраски.

Важно: `MuteInfo`/`MuteWarning` управляют **включением через настройки** (`Logger.ApplySettings`), который применяется автоматически при старте игры (`RuntimeInitializeOnLoadMethod`) только к логгерам, объявленным в `Log.Generated.cs`. Логгеры, созданные вручную через `new Logger("TAG")` в вашем коде (см. раздел выше), в этот процесс не попадают и всегда включены на обоих уровнях.

## Подписка на ошибки — `Logger.OnError`

Статическое событие, срабатывающее на **любой** вызов `Error(...)` любого логгера в проекте — удобно для аналитики/крашрепортинга. Подписка/отписка — в стиле проекта:

```csharp
public sealed class ErrorReporter : IDisposable
{
    public ErrorReporter()
    {
        Logger.OnError += OnError;
    }

    public void Dispose()
    {
        Logger.OnError -= OnError;
    }

    private void OnError(string message)
    {
        // отправить в аналитику/крашрепортинг
    }
}
```

## Ограничения

- Нет файлового/сетевого sink — только `Debug.Log*`.
- Нет интеграции с Zenject/DI — логгеры создаются напрямую через `new Logger(...)`.
- Нет уровней `Debug`/`Trace`/`Fatal` — только `Info`/`Warning`/`Error`.
- Конфиг (`LoggingSettings.asset`) должен лежать в папке `Resources`, иначе рантайм-инициализация не найдёт настройки.

## Пример в проекте

`Assets/_ShootemUP/Scripts/GameCycle/GameCycleManager.cs` использует канал `GAME_CYCLE` для логирования переходов состояний игры (`StartGame`/`PauseGame`/`ResumeGame`/`FinishGame`) — см. код как референс использования.
