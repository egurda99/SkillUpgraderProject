using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class MoveByTransformBehaviour : IEntityInit, IEntityUpdate
{
    private Transform _root;
    private ReactiveVariable<float> _speed;
    private ReactiveVariable<Vector3> _direction;
    private AndExpression _canMove;
    private ReactiveVariable<bool> _isMoving;

    public void Init(IEntity entity)
    {
        _root = entity.GetRootTransform();
        _speed = entity.GetMoveSpeed();
        _direction = entity.GetMoveDirection();
        _isMoving = entity.GetIsMoving();
        _canMove = entity.GetCanMove();
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canMove.Value)
        {
            if (_direction.Value.sqrMagnitude > 0)
            {
                _isMoving.Value = true;
            }

            else
            {
                _isMoving.Value = false;
            }
            _root.position += _direction.Value * _speed.Value * deltaTime;
        }

        else
        {
            _isMoving.Value = false;

        }

    }
}
