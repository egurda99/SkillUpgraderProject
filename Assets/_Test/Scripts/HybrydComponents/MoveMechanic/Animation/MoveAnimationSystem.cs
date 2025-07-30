using System;
using UnityEngine;

namespace MyCodeBase.HybridComponents
{
    public sealed class MoveAnimationSystem : IDisposable
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private readonly MoveData _moveData;
        private readonly Animator _animator;

        public MoveAnimationSystem(MoveData moveData, Animator animator)
        {
            _moveData = moveData;
            _animator = animator;

            _moveData.OnIsMovingChanged += OnIsMovingChanged;
        }

        private void OnIsMovingChanged(bool value)
        {
            _animator.SetBool(IsMoving, value);
        }


        public void Dispose()
        {
            _moveData.OnIsMovingChanged -= OnIsMovingChanged;
        }
    }
}
