using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletFactory : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;

        private BulletPool _bulletPool;

        [Inject]
        public void Construct(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public Bullet GetBullet(Transform parent)
        {
            var bullet = Instantiate(_bulletPrefab, parent);
            var bulletDamageController = new BulletDamageController(bullet, _bulletPool);
            Debug.Log($"<color=red>Controller created with args: {bullet}, + {_bulletPool} < /color>");
            return bullet;
        }
    }
}
