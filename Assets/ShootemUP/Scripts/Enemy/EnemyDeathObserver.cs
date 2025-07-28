using System;
using ShootEmUp;

public sealed class EnemyDeathObserver : IDisposable
{
    private readonly HealthComponent _healthComponent;
    private readonly Enemy.Pool _enemyPool;

    private readonly Enemy _enemy;

    public EnemyDeathObserver(HealthComponent healthComponent, Enemy.Pool pool, Enemy enemy)
    {
        _healthComponent = healthComponent;
        _enemyPool = pool;
        _enemy = enemy;

        _healthComponent.OnDead += OnEnemyDeath;
    }

    private void OnEnemyDeath() => _enemyPool.Despawn(_enemy);

    void IDisposable.Dispose()
    {
        _healthComponent.OnDead -= OnEnemyDeath;
    }
}
