using ShootemUP;
using ShootEmUp;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(HealthComponent))]
public sealed class EnemyDeathObserver : MonoBehaviour,
    IGameStartListener,
    IGameFinishListener
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

    void IGameStartListener.OnStartGame() => _healthComponent.OnDead += OnEnemyDeath;

    void IGameFinishListener.OnFinishGame() => _healthComponent.OnDead -= OnEnemyDeath;

    private void OnEnemyDeath() => _enemyPool.UnspawnEnemy(_enemy);
}
