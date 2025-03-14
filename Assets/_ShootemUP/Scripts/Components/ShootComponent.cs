using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(TeamComponent))]
    public sealed class ShootComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        private BulletPool _bulletPool;

        private TeamComponent _teamComponent;

        private const string BULLETPOOL = "BulletPool";

        private void Awake()
        {
            _teamComponent = GetComponent<TeamComponent>();

            _bulletPool = GameObject.FindGameObjectWithTag(BULLETPOOL).GetComponent<BulletPool>();
        }

        public void Shoot(Vector2 direction)
        {
            _bulletPool.SpawnBullet(_firePoint.position, _teamComponent.IsPlayer, direction);
        }

        public Vector2 GetShootPosition() => _firePoint.position;
    }
}
