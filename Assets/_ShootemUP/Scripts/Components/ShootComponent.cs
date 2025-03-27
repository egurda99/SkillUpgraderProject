using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(typeof(TeamComponent))]
    public sealed class ShootComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        private Bullet.Pool _bulletPool;

        private TeamComponent _teamComponent;

        [Inject]
        public void Construct(Bullet.Pool bulletCunfigurer)
        {
            _bulletPool = bulletCunfigurer;
        }

        private void Awake()
        {
            _teamComponent = GetComponent<TeamComponent>();
        }

        public void Shoot(Vector2 direction)
        {
            _bulletPool.Spawn(_firePoint.position, _teamComponent.IsPlayer, direction);
        }

        public Vector2 GetShootPosition() => _firePoint.position;
    }
}
