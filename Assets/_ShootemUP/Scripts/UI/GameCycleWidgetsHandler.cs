using ShootEmUp;

public sealed class GameCycleWidgetsHandler
{
    private readonly StartGameWidget _startGameWidget;
    private readonly PauseGameWidget _pauseGameWidget;
    private readonly TimerBeforeStartWidget _timerBeforeStartWidget;

    private readonly GameCycleManager _gameCycleManager;

    public GameCycleWidgetsHandler(StartGameWidget startGameWidget, PauseGameWidget pauseGameWidget,
        GameCycleManager gameCycleManager, TimerBeforeStartWidget timerBeforeStartWidget)
    {
        _startGameWidget = startGameWidget;
        _pauseGameWidget = pauseGameWidget;
        _gameCycleManager = gameCycleManager;
        _timerBeforeStartWidget = timerBeforeStartWidget;

        _startGameWidget.OnStartGameButtonPressed += StartButtonPressed;
        _pauseGameWidget.OnPauseGameButtonPressed += PauseButtonPressed;
        _timerBeforeStartWidget.OnTimerEnded += TimerBeforeStartEnded;

        _pauseGameWidget.HideButton();
    }

    private void PauseButtonPressed()
    {
        _gameCycleManager.PauseGame();
        _pauseGameWidget.HideButton();
        _startGameWidget.ShowButton();
    }


    private void TimerBeforeStartEnded()
    {
        if (_gameCycleManager.GameState == GameState.OFF)
        {
            _gameCycleManager.StartGame();
        }

        else
        {
            _gameCycleManager.ResumeGame();
        }

        _pauseGameWidget.ShowButton();
    }


    private void StartButtonPressed()
    {
        _startGameWidget.HideButton();
        _timerBeforeStartWidget.StartTimer();
    }


    ~GameCycleWidgetsHandler()
    {
        _startGameWidget.OnStartGameButtonPressed -= StartButtonPressed;
        _pauseGameWidget.OnPauseGameButtonPressed -= PauseButtonPressed;
        _timerBeforeStartWidget.OnTimerEnded -= TimerBeforeStartEnded;
    }
}
