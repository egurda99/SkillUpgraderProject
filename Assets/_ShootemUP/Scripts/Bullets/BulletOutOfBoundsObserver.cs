using System;

namespace ShootEmUp
{
    public sealed class BulletOutOfBoundsObserver : IDisposable
    {
        private readonly BulletPool _bulletPool;
        private readonly BulletOutOfBoundsChecker _checker;

        public BulletOutOfBoundsObserver(BulletPool bulletPool, BulletOutOfBoundsChecker checker)
        {
            _bulletPool = bulletPool;
            _checker = checker;

            _checker.OnBulletOutOfBound += RemoveBullet;
        }

        void IDisposable.Dispose()
        {
            _checker.OnBulletOutOfBound -= RemoveBullet;
        }

        private void RemoveBullet(Bullet bullet) => _bulletPool.DespawnBullet(bullet);
    }
}
