using System;
using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class ActiveBulletsProvider
    {
        private readonly BulletPool _bulletPool;
        private readonly List<Bullet> _activeBullets = new();

        public IReadOnlyList<Bullet> ActiveBullets => _activeBullets;

        public event Action ActiveBulletsChanged;

        public ActiveBulletsProvider(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
            _bulletPool.OnBulletSpawned += AddBulletToActiveList;
            _bulletPool.OnBulletDespawned += RemoveBulletFromActiveList;
        }

        private void RemoveBulletFromActiveList(Bullet bullet)
        {
            _activeBullets.Remove(bullet);
            ActiveBulletsChanged?.Invoke();
        }

        private void AddBulletToActiveList(Bullet bullet)
        {
            _activeBullets.Add(bullet);
            ActiveBulletsChanged?.Invoke();
        }

        ~ActiveBulletsProvider()
        {
            _bulletPool.OnBulletSpawned -= AddBulletToActiveList;
            _bulletPool.OnBulletDespawned -= RemoveBulletFromActiveList;
        }
    }
}
