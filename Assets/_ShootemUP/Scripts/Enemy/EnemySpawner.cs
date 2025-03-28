using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener,
        IGamePauseListener,
        IGameResumeListener
    {
        private EnemyConfigurer _enemyConfigurer;

        private Coroutine _spawn;
        private readonly float _spawnCooldown = 1;
        private ActiveEnemiesProvider _activeEnemiesProvider;
        [SerializeField] private int _maxEnemiesOnScene = 7;

        private readonly int _adapterForCountFrom1 = 1;

        public void Init(EnemyConfigurer enemyConfigurer, ActiveEnemiesProvider activeEnemiesProvider)
        {
            _enemyConfigurer = enemyConfigurer;
            _activeEnemiesProvider = activeEnemiesProvider;
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
                yield return new WaitForSeconds(_spawnCooldown);

                if (_activeEnemiesProvider.ActiveEnemies.Count + _adapterForCountFrom1 <= _maxEnemiesOnScene)
                    _enemyConfigurer.CreateEnemy();
            }
        }
    }
}
