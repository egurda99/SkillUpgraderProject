using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class RotateToMoveDirectionMechanic : IEntityInstaller
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private float _rotateSpeed = 1f;
    [SerializeField] private bool _isRotating;


    public void Install(IEntity entity)
    {
        entity.AddRootTransform(_rootTransform);
        entity.AddRotationSpeed(_rotateSpeed);
        entity.AddIsRotating(_isRotating);
        var canRotate = new AndExpression();
        entity.AddCanRotate(canRotate);
        entity.AddBehaviour(new RotateToMoveDirectionBehaviour());
    }
}
