using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletOutOfBoundsChecker : MonoBehaviour
    {
        [SerializeField] private LevelBounds _levelBounds;
        private ActiveBulletsProvider _activeBulletsProvider;

        private readonly List<Bullet> _activeBullets = new();
        public event Action<Bullet> OnBulletOutOfBound;

        public void Init(ActiveBulletsProvider activeBulletsProvider)
        {
            _activeBulletsProvider = activeBulletsProvider;
        }

        private void FixedUpdate()
        {
            UpdateActiveBullets();

            for (var i = 0; i < _activeBullets.Count; i++)
            {
                var bullet = _activeBullets[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                    OnBulletOutOfBound?.Invoke(bullet);
            }
        }

        private void UpdateActiveBullets()
        {
            _activeBullets.Clear();
            _activeBullets.AddRange(_activeBulletsProvider.ActiveBullets);
        }
    }
}
