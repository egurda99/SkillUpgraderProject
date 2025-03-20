using System.Collections.Generic;
using ShootemUP;

namespace ShootEmUp
{
    public sealed class BulletsGameCycleUpdater : IGameStartListener,
        IGameFinishListener,
        IGamePauseListener,
        IGameResumeListener
    {
        private readonly ActiveBulletsProvider _activeBulletsProvider;

        private readonly List<IGameListener> _bulletListeners = new();
        private readonly List<Bullet> _activeBullets = new();

        public BulletsGameCycleUpdater(ActiveBulletsProvider activeBulletsProvider)
        {
            _activeBulletsProvider = activeBulletsProvider;
            _activeBulletsProvider.ActiveBulletsChanged += UpdateActiveBullets;
        }

        void IGameStartListener.OnStartGame()
        {
            foreach (var listener in _bulletListeners)
            {
                if (listener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }
        }

        void IGameFinishListener.OnFinishGame()
        {
            foreach (var listener in _bulletListeners)
            {
                if (listener is IGameFinishListener gameFinishListener)
                {
                    gameFinishListener.OnFinishGame();
                }
            }

            _activeBulletsProvider.ActiveBulletsChanged -= UpdateActiveBullets;
        }

        void IGamePauseListener.OnPauseGame()
        {
            foreach (var listener in _bulletListeners)
            {
                if (listener is IGamePauseListener gamePauseListener)
                {
                    gamePauseListener.OnPauseGame();
                }
            }
        }

        void IGameResumeListener.OnResumeGame()
        {
            foreach (var listener in _bulletListeners)
            {
                if (listener is IGameResumeListener gameResumeListener)
                {
                    gameResumeListener.OnResumeGame();
                }
            }
        }

        private void UpdateActiveBullets()
        {
            _activeBullets.Clear();
            _activeBullets.AddRange(_activeBulletsProvider.ActiveBullets);

            _bulletListeners.Clear();

            foreach (var bullet in _activeBullets)
            {
                _bulletListeners.AddRange(bullet.GetComponents<IGameListener>());
            }
        }
    }
}
