using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(typeof(TeamComponent))]
    public sealed class ShootComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        private BulletPool _bulletPool;

        private TeamComponent _teamComponent;

        [Inject]
        public void Construct(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        private void Awake()
        {
            _teamComponent = GetComponent<TeamComponent>();
        }

        public void Shoot(Vector2 direction)
        {
            _bulletPool.SpawnBullet(_firePoint.position, _teamComponent.IsPlayer, direction);
        }

        public Vector2 GetShootPosition() => _firePoint.position;
    }
}
