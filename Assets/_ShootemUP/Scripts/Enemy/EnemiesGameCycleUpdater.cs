using System.Collections.Generic;
using ShootemUP;

namespace ShootEmUp
{
    public sealed class EnemiesGameCycleUpdater : IGameStartListener,
        IGameFinishListener,
        IGamePauseListener,
        IGameResumeListener,
        IGameUpdateListener,
        IGameFixedUpdateListener
    {
        private readonly ActiveEnemiesProvider _activeEnemiesProvider;

        private readonly List<IGameListener> _enemiesListeners = new();
        private readonly List<IGameUpdateListener> _gameUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();

        private readonly List<Enemy> _activeEnemies = new();

        public EnemiesGameCycleUpdater(ActiveEnemiesProvider activeEnemiesProvider)
        {
            _activeEnemiesProvider = activeEnemiesProvider;
            _activeEnemiesProvider.ActiveEnemiesChanged += UpdateActiveEnemies;
        }

        void IGameStartListener.OnStartGame()
        {
            foreach (var listener in _enemiesListeners)
            {
                if (listener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }
        }

        void IGameFinishListener.OnFinishGame()
        {
            foreach (var listener in _enemiesListeners)
            {
                if (listener is IGameFinishListener gameFinishListener)
                {
                    gameFinishListener.OnFinishGame();
                }
            }

            _activeEnemiesProvider.ActiveEnemiesChanged -= UpdateActiveEnemies;
        }

        void IGamePauseListener.OnPauseGame()
        {
            foreach (var listener in _enemiesListeners)
            {
                if (listener is IGamePauseListener gamePauseListener)
                {
                    gamePauseListener.OnPauseGame();
                }
            }
        }

        void IGameResumeListener.OnResumeGame()
        {
            foreach (var listener in _enemiesListeners)
            {
                if (listener is IGameResumeListener gameResumeListener)
                {
                    gameResumeListener.OnResumeGame();
                }
            }
        }

        public void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < _gameUpdateListeners.Count; i++)
            {
                var listener = _gameUpdateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }


        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            for (var i = 0; i < _gameFixedUpdateListeners.Count; i++)
            {
                var listener = _gameFixedUpdateListeners[i];
                listener.OnFixedUpdate(fixedDeltaTime);
            }
        }

        private void UpdateActiveEnemies()
        {
            _activeEnemies.Clear();
            _activeEnemies.AddRange(_activeEnemiesProvider.ActiveEnemies);

            _enemiesListeners.Clear();

            foreach (var enemy in _activeEnemies)
            {
                var componentsListeners = enemy.GetComponents<IGameListener>();

                _enemiesListeners.AddRange(componentsListeners);

                foreach (var componentsListener in componentsListeners)
                {
                    if (componentsListener is IGameUpdateListener gameUpdateListener)
                    {
                        _gameUpdateListeners.Add(gameUpdateListener);
                    }

                    if (componentsListener is IGameFixedUpdateListener gameFixedUpdateListener)
                    {
                        _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
                    }
                }
            }
        }
    }
}
