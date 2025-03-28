using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameCycleManager : MonoBehaviour
    {
        private readonly List<IGameListener> _gameListeners = new();

        private readonly List<IGameUpdateListener> _gameUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> _gameLateUpdateListeners = new();


        private DiContainer _container;

        private GameState _gameState;
        public GameState GameState => _gameState;


        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        private void Awake()
        {
            foreach (var gameUpdateListener in _container.Resolve<IEnumerable<IGameUpdateListener>>())
            {
                _gameUpdateListeners.Add(gameUpdateListener);
            }

            foreach (var gameFixedUpdateListener in _container.Resolve<IEnumerable<IGameFixedUpdateListener>>())
            {
                _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
            }

            foreach (var gameLateUpdateListener in _container.Resolve<IEnumerable<IGameLateUpdateListener>>())
            {
                _gameLateUpdateListeners.Add(gameLateUpdateListener);
            }
        }

        private void Update()
        {
            if (_gameState != GameState.PLAYING)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _gameUpdateListeners.Count; i++)
            {
                var listener = _gameUpdateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (_gameState != GameState.PLAYING)
            {
                return;
            }

            var deltaTime = Time.fixedDeltaTime;
            for (var i = 0; i < _gameFixedUpdateListeners.Count; i++)
            {
                var listener = _gameFixedUpdateListeners[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;

            for (var i = 0; i < _gameLateUpdateListeners.Count; i++)
            {
                var listener = _gameLateUpdateListeners[i];
                listener.OnLateUpdate(deltaTime);
            }
        }

        public void AddListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }

            _gameListeners.Add(listener);

            if (listener is IGameUpdateListener gameUpdateListener)
            {
                _gameUpdateListeners.Add(gameUpdateListener);
            }

            if (listener is IGameFixedUpdateListener gameFixedUpdateListener)
            {
                _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener gameLateUpdateListener)
            {
                _gameLateUpdateListeners.Add(gameLateUpdateListener);
            }
        }


        [Button]
        public void StartGame()
        {
            if (_gameState != GameState.OFF)
            {
                return;
            }

            Debug.Log("<color=green>Game started</color>");

            foreach (var gameStartListener in _container.Resolve<IEnumerable<IGameStartListener>>())
            {
                gameStartListener.OnStartGame();
            }


            foreach (var listener in _gameListeners)
            {
                if (listener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }


            _gameState = GameState.PLAYING;
        }

        [Button]
        public void PauseGame()
        {
            if (_gameState != GameState.PLAYING)
            {
                return;
            }

            Debug.Log("<color=green>Game paused</color>");

            foreach (var gamePauseListener in _container.Resolve<IEnumerable<IGamePauseListener>>())
            {
                gamePauseListener.OnPauseGame();
            }

            foreach (var listener in _gameListeners)
            {
                if (listener is IGamePauseListener gamePauseListener)
                {
                    gamePauseListener.OnPauseGame();
                }
            }

            _gameState = GameState.PAUSED;
        }

        [Button]
        public void ResumeGame()
        {
            if (_gameState != GameState.PAUSED)
            {
                return;
            }

            Debug.Log("<color=green>Game resumed</color>");

            foreach (var gameResumeListener in _container.Resolve<IEnumerable<IGameResumeListener>>())
            {
                gameResumeListener.OnResumeGame();
            }

            foreach (var listener in _gameListeners)
            {
                if (listener is IGameResumeListener gameResumeListener)
                {
                    gameResumeListener.OnResumeGame();
                }
            }

            _gameState = GameState.PLAYING;
        }

        [Button]
        public void FinishGame()
        {
            if (_gameState != GameState.PLAYING)
            {
                return;
            }

            Debug.Log("<color=green>Game finished</color>");

            foreach (var gameFinishListener in _container.Resolve<IEnumerable<IGameFinishListener>>())
            {
                gameFinishListener.OnFinishGame();
            }

            foreach (var listener in _gameListeners)
            {
                if (listener is IGameFinishListener gameFinishListener)
                {
                    gameFinishListener.OnFinishGame();
                }
            }

            _gameState = GameState.FINISHED;
        }
    }
}
