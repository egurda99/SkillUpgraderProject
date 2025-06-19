using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public class AutoMoveToTargetMechanic : IEntityInstaller
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private Transform _target;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _stopDistance = 0.1f;
    [SerializeField] private bool _isMoving;
    [SerializeField] private bool _canMove = true;

    public void Install(IEntity entity)
    {
        entity.AddRootTransform(_rootTransform);
        entity.AddMoveSpeed(_speed);
        entity.AddTarget(_target);
        entity.AddMoveDirection(new ReactiveVariable<Vector3>());
        entity.AddStopDistance(_stopDistance);


        entity.AddIsMoving(_isMoving);

        var canMove = new AndExpression();
        entity.AddCanMove(canMove);

        entity.AddBehaviour(new AutoMoveToTargetBehaviour());
    }
}
