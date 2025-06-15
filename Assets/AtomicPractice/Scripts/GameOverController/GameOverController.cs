using System;
using Atomic.Entities;

public sealed class GameOverController
{
    private readonly SceneEntity _playerEntity;

    public event Action OnGameEnded;

    public GameOverController(PlayerService service)
    {
        _playerEntity = service.Player;

        _playerEntity.GetIsDead().Subscribe(_ => OnGameEnded?.Invoke());
    }

    ~GameOverController()
    {
        _playerEntity.GetIsDead().Unsubscribe(_ => OnGameEnded?.Invoke());
    }
}
