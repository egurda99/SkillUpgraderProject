namespace ShootEmUp
{
    public sealed class BulletOutOfBoundsObserver
    {
        private readonly Bullet.Pool _bulletPool;
        private readonly BulletOutOfBoundsChecker _checker;

        public BulletOutOfBoundsObserver(Bullet.Pool bulletPool, BulletOutOfBoundsChecker checker)
        {
            _bulletPool = bulletPool;
            _checker = checker;

            _checker.OnBulletOutOfBound += RemoveBullet;
        }

        private void RemoveBullet(Bullet bullet) => _bulletPool.Despawn(bullet);

        ~BulletOutOfBoundsObserver() => _checker.OnBulletOutOfBound -= RemoveBullet;
    }
}
