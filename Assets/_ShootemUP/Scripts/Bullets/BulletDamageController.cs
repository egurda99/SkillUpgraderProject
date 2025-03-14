using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Bullet))]
    public sealed class BulletDamageController : MonoBehaviour
    {
        private const string BULLETPOOL = "BulletPool";
        private Bullet _bullet;
        private BulletPool _bulletPool;

        private void Awake()
        {
            _bulletPool = GameObject.FindGameObjectWithTag(BULLETPOOL).GetComponent<BulletPool>();
            _bullet = GetComponent<Bullet>();
        }

        private void OnEnable() => _bullet.OnCollisionEntered += OnBulletCollision;

        private void OnDisable() => _bullet.OnCollisionEntered -= OnBulletCollision;

        private void OnBulletCollision(Collision2D collision)
        {
            BulletUtils.DealDamage(_bullet, collision.gameObject);
            _bulletPool.DespawnBullet(_bullet);
        }
    }
}
