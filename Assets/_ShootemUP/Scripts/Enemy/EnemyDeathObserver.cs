using ShootEmUp;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(HealthComponent))]
public sealed class EnemyDeathObserver : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private EnemyPool _enemyPool;
    private Enemy _enemy;

    private const string ENEMYSYSTEM = "EnemySystem";

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _enemy = GetComponent<Enemy>();
        var enemySystem = GameObject.FindGameObjectWithTag(ENEMYSYSTEM);
        _enemyPool = enemySystem.GetComponent<EnemyPool>();
    }

    private void OnEnable() => _healthComponent.OnDead += OnEnemyDeath;

    private void OnDisable() => _healthComponent.OnDead -= OnEnemyDeath;

    private void OnEnemyDeath() => _enemyPool.UnspawnEnemy(_enemy);
}
