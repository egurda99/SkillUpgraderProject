using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;
        [Header("PoolContainer")] [SerializeField] private Transform _container;

        private readonly Queue<Enemy> _enemyPool = new();
        private readonly int _initialCount = 7;

        private void Awake()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var enemy = _enemyFactory.GetEnemy(_container);
                _enemyPool.Enqueue(enemy);
                ToggleActiveStatus(enemy.gameObject, false);
            }
        }

        public Enemy SpawnEnemy()
        {
            if (!_enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            ToggleActiveStatus(enemy.gameObject, true);

            return enemy;
        }

        public void UnspawnEnemy(Enemy enemy)
        {
            enemy.transform.SetParent(_container);
            ToggleActiveStatus(enemy.gameObject, false);
            _enemyPool.Enqueue(enemy);
        }

        private void ToggleActiveStatus(GameObject enemy, bool isActive)
        {
            enemy.SetActive(isActive);
        }
    }
}
