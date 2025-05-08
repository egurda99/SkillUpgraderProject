using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public class MoveByTransformMechanic : IEntityInstaller
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Vector3 _moveDirection = Vector3.zero;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _canMove = true;

    public void Install(IEntity entity)
    {
        entity.AddRootTransform(_rootTransform);
        entity.AddMoveSpeed(_speed);
        entity.AddMoveDirection(_moveDirection);

        entity.AddIsMoving(_isMoving);

        var canMove = new AndExpression();
        entity.AddCanMove(canMove);

        entity.AddBehaviour(new MoveByTransformBehaviour());

    }
}
