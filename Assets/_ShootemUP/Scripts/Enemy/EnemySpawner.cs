using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;

        private EnemyInstaller _enemyInstaller;
        private readonly float _spawnCooldown = 1;

        public void Init(EnemyInstaller enemyInstaller)
        {
            _enemyInstaller = enemyInstaller;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnCooldown);
                var enemy = _enemyPool.SpawnEnemy();

                if (enemy != null)
                {
                    _enemyInstaller.ConfigureEnemy(enemy);
                }
            }
        }
    }
}
