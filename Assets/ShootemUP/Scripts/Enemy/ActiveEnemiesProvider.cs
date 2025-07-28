using System;
using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class ActiveEnemiesProvider
    {
        private readonly Enemy.Pool _enemyPool;
        private readonly List<Enemy> _activeEnemies = new();

        public IReadOnlyList<Enemy> ActiveEnemies => _activeEnemies;

        public event Action ActiveEnemiesChanged;

        public ActiveEnemiesProvider(Enemy.Pool enemyPool)
        {
            _enemyPool = enemyPool;
            _enemyPool.EnemySpawned += AddEnemyToActiveList;
            _enemyPool.EnemyDespawned += RemoveEnemyFromActiveList;
        }

        private void RemoveEnemyFromActiveList(Enemy enemy)
        {
            _activeEnemies.Remove(enemy);
            ActiveEnemiesChanged?.Invoke();
        }

        private void AddEnemyToActiveList(Enemy enemy)
        {
            _activeEnemies.Add(enemy);
            ActiveEnemiesChanged?.Invoke();
        }

        ~ActiveEnemiesProvider()
        {
            _enemyPool.EnemySpawned -= AddEnemyToActiveList;
            _enemyPool.EnemyDespawned -= RemoveEnemyFromActiveList;
        }
    }
}
