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
    private ReactiveVariable<float> _stopDistance;

    public void Init(IEntity entity)
    {
        _root = entity.GetRootTransform();
        _speed = entity.GetMoveSpeed();
        _target = entity.GetTarget();
        _isMoving = entity.GetIsMoving();
        _canMove = entity.GetCanMove();
        _moveDirection = entity.GetMoveDirection();
        _stopDistance = entity.GetStopDistance();
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canMove.Value)
        {
            var worldDirection = _target.Value.position - _root.position;

            var sqrDistance = worldDirection.sqrMagnitude;
            var stopThresholdSqr = _stopDistance.Value * _stopDistance.Value;
            if (sqrDistance > stopThresholdSqr)
            {
                _isMoving.Value = true;
                _moveDirection.Value = worldDirection.normalized;
                _root.position += worldDirection.normalized * _speed.Value * deltaTime;
            }
            else
            {
                _isMoving.Value = false;
                _moveDirection.Value = Vector3.zero;
            }
        }
        else
        {
            _isMoving.Value = false;
            _moveDirection.Value = Vector3.zero;
        }
    }
}
