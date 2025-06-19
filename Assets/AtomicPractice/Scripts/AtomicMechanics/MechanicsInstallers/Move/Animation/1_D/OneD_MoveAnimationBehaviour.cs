using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class OneD_MoveAnimationBehaviour : IEntityInit, IEntityDispose
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private Animator _animator;
    private ReactiveVariable<bool> _isMoving;

    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _isMoving = entity.GetIsMoving();
        _isMoving.Subscribe(OnIsMovingChanged);
    }

    private void OnIsMovingChanged(bool value)
    {
        _animator.SetBool(IsMoving, value);
    }

    public void Dispose(IEntity entity)
    {
        _isMoving.Unsubscribe(OnIsMovingChanged);
    }
}
