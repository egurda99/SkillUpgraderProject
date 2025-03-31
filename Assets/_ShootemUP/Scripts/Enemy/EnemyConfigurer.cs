using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyConfigurer
    {
        private readonly Transform _enemyTargetTransform;
        private readonly EnemyPositionsHandler _enemyPositionsHandler;


        private readonly Enemy.Pool _enemyPool;

        public EnemyConfigurer(Player player, EnemyPositionsHandler enemyPositionsHandler, Enemy.Pool enemyPool)
        {
            _enemyTargetTransform = player.transform;
            _enemyPositionsHandler = enemyPositionsHandler;

            _enemyPool = enemyPool;
        }


        public void CreateEnemy()
        {
            _enemyPool.Spawn(_enemyTargetTransform, _enemyPositionsHandler.GetRandomAttackPosition().position,
                _enemyPositionsHandler.GetRandomSpawnPosition().position);
        }
    }
}
