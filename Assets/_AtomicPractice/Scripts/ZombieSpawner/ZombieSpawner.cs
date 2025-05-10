using System;
using Atomic.Entities;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private SceneEntity _zombiePrefab;

    private Transform _target;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval = 2f;

    private Timer _spawnTimer;
    private GameEndController _gameController;
    private bool _isGameEnded;

    [Inject]
    public void Construct(SceneEntity entity, GameEndController gameEndController)
    {
        _target = entity.transform;
        _gameController = gameEndController;
    }

    private void Awake()
    {
        _spawnTimer = new Timer(_spawnInterval);
    }

    private void OnEnable()
    {
        _spawnTimer.OnElapsed += OnSpawnTimerEnded;
        _gameController.OnGameEnded += OnGameEnded;
    }

    private void OnGameEnded()
    {
        _isGameEnded = true;
    }

    private void OnDisable()
    {
        _spawnTimer.OnElapsed -= OnSpawnTimerEnded;
        _gameController.OnGameEnded -= OnGameEnded;
    }

    private void Update()
    {
        if (_isGameEnded)
            return;
        _spawnTimer.Update(Time.deltaTime);
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
        var zombieGO = Instantiate(_zombiePrefab, _spawnPoints[index].position, Quaternion.identity);
        zombieGO.GetTarget().Value = _target;
        _spawnTimer.Reset();
        Debug.Log("<color=red>Zombie Spawned</color>");
    }
}
