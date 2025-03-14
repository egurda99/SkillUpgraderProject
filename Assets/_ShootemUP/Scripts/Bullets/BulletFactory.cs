using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletFactory : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;

        public Bullet GetBullet(Transform parent)
        {
            var bullet = Instantiate(_bulletPrefab, parent);
            return bullet;
        }
    }
}
