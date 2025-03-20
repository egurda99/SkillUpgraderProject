using ShootemUP;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Bullet))]
    public sealed class BulletDamageController : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        private const string BULLETPOOL = "BulletPool";
        private Bullet _bullet;
        private BulletPool _bulletPool;

        private void Awake()
        {
            _bulletPool = GameObject.FindGameObjectWithTag(BULLETPOOL).GetComponent<BulletPool>();
            _bullet = GetComponent<Bullet>();
        }


        void IGameStartListener.OnStartGame()
        {
            Debug.Log("<color=orange>bulletStart</color>");
            _bullet.OnCollisionEntered += OnBulletCollision;
        }

        void IGameFinishListener.OnFinishGame()
        {
            Debug.Log("<color=orange>bulletEnd</color>");
            _bullet.OnCollisionEntered -= OnBulletCollision;
        }

        private void OnBulletCollision(Collision2D collision)
        {
            BulletUtils.DealDamage(_bullet, collision.gameObject);
            _bulletPool.DespawnBullet(_bullet);
        }
    }
}
