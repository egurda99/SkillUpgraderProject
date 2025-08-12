using System;
using UnityEngine;

namespace MyCodeBase.HybridComponents
{
    public sealed class DeathAnimationSystem : IDisposable
    {
        private static readonly int IsAlive = Animator.StringToHash("IsAlive");
        private readonly HealthData _healthData;
        private readonly Animator _animator;

        public DeathAnimationSystem(HealthData healthData, Animator animator)
        {
            _healthData = healthData;
            _animator = animator;

            _healthData.OnIsAliveChanged += OnIsAliveChanged;

            OnIsAliveChanged(_healthData.IsAlive);
        }

        private void OnIsAliveChanged(bool value)
        {
            _animator.SetBool(IsAlive, value);
        }


        public void Dispose()
        {
            _healthData.OnIsAliveChanged += OnIsAliveChanged;
        }
    }
}
