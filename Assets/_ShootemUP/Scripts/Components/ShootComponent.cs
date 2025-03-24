using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(TeamComponent))]
    public sealed class ShootComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        private BulletPool _bulletPool;

        private TeamComponent _teamComponent;

        private void Awake()
        {
            _teamComponent = GetComponent<TeamComponent>();

            _bulletPool = FindObjectOfType<BulletPool>();

            if (_bulletPool == null)
            {
                throw new Exception("BulletPool could not be found");
            }
        }

        public void Shoot(Vector2 direction)
        {
            _bulletPool.SpawnBullet(_firePoint.position, _teamComponent.IsPlayer, direction);
        }

        public Vector2 GetShootPosition() => _firePoint.position;
    }
}
