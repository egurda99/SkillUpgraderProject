using System;
using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class BulletOutOfBoundsChecker : IGameFixedUpdateListener, IGameFinishListener

    {
        private readonly List<Bullet> _activeBullets = new();
        private readonly ActiveBulletsProvider _activeBulletsProvider;
        private readonly LevelBounds _levelBounds;

        public event Action<Bullet> OnBulletOutOfBound;

        public BulletOutOfBoundsChecker(ActiveBulletsProvider activeBulletsProvider, LevelBounds levelBounds)
        {
            _activeBulletsProvider = activeBulletsProvider;
            _activeBulletsProvider.ActiveBulletsChanged += UpdateActiveBullets;
            _levelBounds = levelBounds;
        }

        void IGameFinishListener.OnFinishGame()
        {
            _activeBulletsProvider.ActiveBulletsChanged -= UpdateActiveBullets;
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            for (var i = 0; i < _activeBullets.Count; i++)
            {
                var bullet = _activeBullets[i];
                if (!_levelBounds.InBounds(bullet.transform.position)) OnBulletOutOfBound?.Invoke(bullet);
            }
        }

        private void UpdateActiveBullets()
        {
            _activeBullets.Clear();
            _activeBullets.AddRange(_activeBulletsProvider.ActiveBullets);
        }
    }
}
