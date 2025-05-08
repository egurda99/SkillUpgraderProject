using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class TwoD_MoveAnimationBehaviour : IEntityInit, IEntityLateUpdate, IEntityDispose
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int MoveHorizontal = Animator.StringToHash("Move_Horizontal");
    private static readonly int MoveVertical = Animator.StringToHash("Move_Vertical");
    private Animator _animator;
    private ReactiveVariable<bool> _isMoving;
    private ReactiveVariable<Vector3> _direction;

    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _isMoving = entity.GetIsMoving();
        _direction = entity.GetMoveDirection();

        _isMoving.Subscribe(OnIsMovingChanged);
    }

    private void OnIsMovingChanged(bool value)
    {
        _animator.SetBool(IsMoving, value);
    }


    public void OnLateUpdate(IEntity entity, float deltaTime)
    {
        if (_isMoving.Value)
        {
            var verticalValue = _direction.Value.z;
            var horizontalValue = _direction.Value.x;

            _animator.SetFloat(MoveHorizontal, horizontalValue);
            _animator.SetFloat(MoveVertical, verticalValue);
        }
    }

    public void Dispose(IEntity entity)
    {
        _isMoving.Unsubscribe(OnIsMovingChanged);
    }
}
