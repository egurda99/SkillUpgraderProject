using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class AutoMoveToTargetBehaviour : IEntityInit, IEntityUpdate
{
    private Transform _root;
    private ReactiveVariable<float> _speed;
    private AndExpression _canMove;
    private ReactiveVariable<bool> _isMoving;
    private ReactiveVariable<Transform> _target;
    private ReactiveVariable<Vector3> _moveDirection;

    public void Init(IEntity entity)
    {
        _root = entity.GetRootTransform();
        _speed = entity.GetMoveSpeed();
        _target = entity.GetTarget();
        _isMoving = entity.GetIsMoving();
        _canMove = entity.GetCanMove();
        _moveDirection = entity.GetMoveDirection();
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canMove.Value)
        {
            var worldDirection = _target.Value.position - _root.position;
            _moveDirection.Value = worldDirection;
            if (worldDirection.sqrMagnitude > 0f)
            {
                _isMoving.Value = true;
                _root.position += worldDirection * _speed.Value * deltaTime;
            }
            else
            {
                _isMoving.Value = false;
            }
        }
        else
        {
            _isMoving.Value = false;
            _moveDirection.Value = Vector3.zero;
        }
    }
}
