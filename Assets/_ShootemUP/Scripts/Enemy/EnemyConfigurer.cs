using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyConfigurer
    {
        private readonly Transform _enemyTarget;
        private readonly EnemyPositionsHandler _enemyPositionsHandler;
        private readonly Bullet.Pool _bulletPool;

        private readonly Enemy.Pool _enemyPool;

        public EnemyConfigurer(Transform enemyTarget, EnemyPositionsHandler enemyPositionsHandler,
            Bullet.Pool bulletPool, Enemy.Pool enemyPool)
        {
            _enemyTarget = enemyTarget;
            _enemyPositionsHandler = enemyPositionsHandler;
            _bulletPool = bulletPool;
            _enemyPool = enemyPool;

            _enemyPool.EnemyCreated += OnEnemyCreated;
        }

        public void CreateEnemy()
        {
            _enemyPool.Spawn(_enemyTarget, _enemyPositionsHandler.GetRandomAttackPosition().position,
                _enemyPositionsHandler.GetRandomSpawnPosition().position);
        }

        private void OnEnemyCreated(Enemy enemy)
        {
            var shootComponent = enemy.GetComponent<ShootComponent>();

            shootComponent.Construct(_bulletPool);
        }
    }
}
