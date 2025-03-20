using ShootEmUp;

public sealed class GameCycleWidgetsController
{
    private readonly StartGameWidget _startGameWidget;
    private readonly PauseGameWidget _pauseGameWidget;
    private readonly GameCycleManager _gameCycleManager;

    public GameCycleWidgetsController(StartGameWidget startGameWidget, PauseGameWidget pauseGameWidget,
        GameCycleManager gameCycleManager)
    {
        _startGameWidget = startGameWidget;
        _pauseGameWidget = pauseGameWidget;
        _gameCycleManager = gameCycleManager;

        _startGameWidget.OnStartGameButtonPressed += StartButtonPressed;
        _pauseGameWidget.OnPauseGameButtonPressed += PauseButtonPressed;

        _pauseGameWidget.HideButton();
    }

    private void PauseButtonPressed()
    {
        _gameCycleManager.PauseGame();
        _pauseGameWidget.HideButton();
        _startGameWidget.ShowButton();
    }

    private void StartButtonPressed()
    {
        if (_gameCycleManager.GameState == GameState.OFF)
        {
            _gameCycleManager.StartGame();
        }

        else
        {
            _gameCycleManager.ResumeGame();
        }


        _startGameWidget.HideButton();
        _pauseGameWidget.ShowButton();
    }


    ~GameCycleWidgetsController()
    {
        _startGameWidget.OnStartGameButtonPressed -= StartButtonPressed;
        _pauseGameWidget.OnPauseGameButtonPressed -= PauseButtonPressed;
    }
}
