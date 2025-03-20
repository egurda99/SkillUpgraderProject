using System.Collections;
using ShootemUP;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField] private EnemyPool _enemyPool;

        private EnemyInstaller _enemyInstaller;

        private Coroutine _spawn;


        public void Init(EnemyInstaller enemyInstaller)
        {
            _enemyInstaller = enemyInstaller;
        }

        void IGameStartListener.OnStartGame()
        {
            _spawn = StartCoroutine(SpawnEnemies());
        }

        void IGameFinishListener.OnFinishGame()
        {
            if (_spawn != null)
                StopCoroutine(_spawn);
        }

        void IGamePauseListener.OnPauseGame()
        {
            if (_spawn != null)
                StopCoroutine(_spawn);
        }

        void IGameResumeListener.OnResumeGame()
        {
            _spawn = StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = _enemyPool.SpawnEnemy();

                if (enemy != null)
                {
                    _enemyInstaller.ConfigureEnemy(enemy);
                }
            }
        }
    }
}
