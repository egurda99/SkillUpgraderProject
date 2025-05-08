using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class RotateMechanic : IEntityInstaller
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotateSpeed = 1f;
    [SerializeField] private bool _isRotating;


    public void Install(IEntity entity)
    {
        entity.AddRootTransform(_rootTransform);
        entity.AddRotationSpeed(_rotateSpeed);
        entity.AddIsRotating(_isRotating);
        entity.AddTarget(_target);

        var canRotate = new AndExpression();

        entity.AddCanRotate(canRotate);

        entity.AddBehaviour(new RotateBehaviour());

    }
}
