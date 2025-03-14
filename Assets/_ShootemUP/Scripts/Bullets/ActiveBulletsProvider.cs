using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class ActiveBulletsProvider
    {
        private readonly BulletPool _bulletPool;
        private readonly List<Bullet> _activeBullets = new();

        public IReadOnlyList<Bullet> ActiveBullets => _activeBullets;

        public ActiveBulletsProvider(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
            _bulletPool.OnBulletSpawned += AddBulletToActiveList;
            _bulletPool.OnBulletDespawned += RemoveBulletFromActiveList;
        }

        private void RemoveBulletFromActiveList(Bullet bullet) => _activeBullets.Remove(bullet);

        private void AddBulletToActiveList(Bullet bullet) => _activeBullets.Add(bullet);

        ~ActiveBulletsProvider()
        {
            _bulletPool.OnBulletSpawned -= AddBulletToActiveList;
            _bulletPool.OnBulletDespawned -= RemoveBulletFromActiveList;
        }
    }
}
