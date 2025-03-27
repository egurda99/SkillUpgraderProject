using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletDamageController
    {
        private readonly Bullet _bullet;
        private readonly BulletPool _bulletPool;

        public BulletDamageController(Bullet bullet, BulletPool bulletPool)
        {
            _bullet = bullet;
            _bulletPool = bulletPool;
            _bullet.OnCollisionEntered += OnBulletCollision;
            Debug.Log($"<color=red>Pool + {_bulletPool}</color>");
        }

        // void IDisposable.Dispose()
        // {
        //     _bullet.OnCollisionEntered -= OnBulletCollision;
        // }

        private void OnBulletCollision(Collision2D collision)
        {
            BulletUtils.DealDamage(_bullet, collision.gameObject);
            _bulletPool.DespawnBullet(_bullet);
        }
    }
}
