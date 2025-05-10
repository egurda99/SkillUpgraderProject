using System;
using Atomic.Entities;

public sealed class GameEndController
{
    private readonly SceneEntity _playerEntity;

    public event Action OnGameEnded;

    public GameEndController(SceneEntity playerEntity)
    {
        _playerEntity = playerEntity;

        _playerEntity.GetIsDead().Subscribe(_ => OnGameEnded?.Invoke());
    }

    ~GameEndController()
    {
        _playerEntity.GetIsDead().Unsubscribe(_ => OnGameEnded?.Invoke());
    }
}