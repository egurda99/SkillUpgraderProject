using System;
using Zenject;

public sealed class GameOverAdapter : IInitializable, IDisposable
{
    private readonly GameOverView _gameOverView;
    private readonly GameOverController _gameOverController;


    public GameOverAdapter(GameOverView gameOverView, GameOverController gameOverController)
    {
        _gameOverView = gameOverView;
        _gameOverController = gameOverController;
    }

    public void Initialize()
    {
        _gameOverController.OnGameEnded += OnGameEnded;
        _gameOverView.Hide();
    }

    private void OnGameEnded()
    {
        _gameOverView.Show();
    }

    public void Dispose()
    {
        _gameOverController.OnGameEnded -= OnGameEnded;
    }
}