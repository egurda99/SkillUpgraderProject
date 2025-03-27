using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyInstaller
    {
        private readonly Transform _enemyTarget;
        private readonly EnemyPositionsHandler _enemyPositionsHandler;
        private readonly BulletPool _bulletPool;

        public EnemyInstaller(Transform enemyTarget, EnemyPositionsHandler enemyPositionsHandler, BulletPool bulletPool)
        {
            _enemyTarget = enemyTarget;
            _enemyPositionsHandler = enemyPositionsHandler;
            _bulletPool = bulletPool;
        }


        public void ConfigureEnemy(Enemy enemy)
        {
            enemy.Init(_enemyTarget, _enemyPositionsHandler.GetRandomAttackPosition().position,
                _enemyPositionsHandler.GetRandomSpawnPosition().position);

            var shootComponent = enemy.GetComponent<ShootComponent>();
            shootComponent.Construct(_bulletPool);
        }
    }
}
