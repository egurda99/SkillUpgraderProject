using System;
using ShootEmUp;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(HealthComponent))]
public sealed class EnemyDeathObserver : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private EnemyPool _enemyPool;
    private Enemy _enemy;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _enemy = GetComponent<Enemy>();

        _enemyPool = FindObjectOfType<EnemyPool>();

        if (_enemyPool == null)
        {
            throw new Exception("EnemyPool could not be found");
        }
    }

    private void OnEnable() => _healthComponent.OnDead += OnEnemyDeath;

    private void OnDisable() => _healthComponent.OnDead -= OnEnemyDeath;

    private void OnEnemyDeath() => _enemyPool.UnspawnEnemy(_enemy);
}
