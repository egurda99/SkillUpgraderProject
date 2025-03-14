using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyInstaller
    {
        private readonly Transform _enemyTarget;
        private readonly EnemyPositionsHandler _enemyPositionsHandler;

        public EnemyInstaller(Transform enemyTarget, EnemyPositionsHandler enemyPositionsHandler)
        {
            _enemyTarget = enemyTarget;
            _enemyPositionsHandler = enemyPositionsHandler;
        }

        public void ConfigureEnemy(Enemy enemy)
        {
            enemy.Init(_enemyTarget, _enemyPositionsHandler.GetRandomAttackPosition().position,
                _enemyPositionsHandler.GetRandomSpawnPosition().position);
        }
    }
}
