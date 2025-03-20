using System;
using System.Collections.Generic;
using ShootemUP;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletOutOfBoundsChecker : MonoBehaviour,
        IGameFixedUpdateListener,
        IGameFinishListener

    {
        [SerializeField] private LevelBounds _levelBounds;

        private readonly List<Bullet> _activeBullets = new();
        private ActiveBulletsProvider _activeBulletsProvider;

        public event Action<Bullet> OnBulletOutOfBound;


        public void Init(ActiveBulletsProvider activeBulletsProvider)
        {
            _activeBulletsProvider = activeBulletsProvider;
            _activeBulletsProvider.ActiveBulletsChanged += UpdateActiveBullets;
        }

        void IGameFinishListener.OnFinishGame()
        {
            _activeBulletsProvider.ActiveBulletsChanged -= UpdateActiveBullets;
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            for (var i = 0; i < _activeBullets.Count; i++)
            {
                var bullet = _activeBullets[i];
                if (!_levelBounds.InBounds(bullet.transform.position)) OnBulletOutOfBound?.Invoke(bullet);
            }
        }


        private void UpdateActiveBullets()
        {
            _activeBullets.Clear();
            _activeBullets.AddRange(_activeBulletsProvider.ActiveBullets);
        }
    }
}
