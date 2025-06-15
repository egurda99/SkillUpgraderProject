using System;
using Atomic.Entities;
using MyTimer;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public sealed class ZombieSpawner : IInitializable, IDisposable
{
    private readonly SceneEntity _zombiePrefab;

    private readonly Transform _target;

    private readonly Transform[] _spawnPoints;
    private readonly float _spawnInterval;

    private readonly Timer _spawnTimer;
    private readonly GameOverController _gameController;
    private bool _isGameEnded;
    private readonly PlayerService _playerService;
    private readonly Transform _zombieContainer;

    private Atomic.Elements.Timer _testTimer;

    public event Action<SceneEntity> OnZombieSpawned;

    public ZombieSpawner(SceneEntity zombiePrefab, Transform[] spawnPoints, float spawnInterval,
        GameOverController gameController, PlayerService playerService, Timer spawnTimer, Transform zombieContainer)
    {
        _zombiePrefab = zombiePrefab;
        _spawnPoints = spawnPoints;
        _spawnInterval = spawnInterval;
        _playerService = playerService;
        _target = _playerService.Player.transform;
        _gameController = gameController;
        _spawnTimer = spawnTimer;
        _zombieContainer = zombieContainer;
    }


    public void Initialize()
    {
        _spawnTimer.OnElapsed += OnSpawnTimerEnded;
        _gameController.OnGameEnded += OnGameEnded;

        _spawnTimer.SetInterval(_spawnInterval);
        _spawnTimer.Start();
    }


    private void OnGameEnded()
    {
        _isGameEnded = true;
    }


    private void OnSpawnTimerEnded()
    {
        if (_zombiePrefab == null || _spawnPoints.Length == 0)
        {
            throw new Exception("Zombie prefab is null or spawn points is empty.");
        }

        if (_isGameEnded)
            return;


        var index = Random.Range(0, _spawnPoints.Length);
        var zombieGO = Object.Instantiate(_zombiePrefab, _spawnPoints[index].position, Quaternion.identity,
            _zombieContainer);
        OnZombieSpawned?.Invoke(zombieGO);
        zombieGO.GetTarget().Value = _target;
        _spawnTimer.Reset();
        Debug.Log("<color=red>Zombie Spawned</color>");
    }

    public void Dispose()
    {
        _spawnTimer.OnElapsed -= OnSpawnTimerEnded;
        _gameController.OnGameEnded -= OnGameEnded;
    }
}
