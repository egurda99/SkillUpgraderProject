using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public class RotateByMouseMechanic : IEntityInstaller
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private float _rotateSpeed = 100f;

    public void Install(IEntity entity)
    {
        entity.AddRootTransform(_rootTransform);
        entity.AddRotationSpeed(_rotateSpeed);
        entity.AddIsRotating(new ReactiveVariable<bool>());
        entity.AddTargetPosition(new ReactiveVariable<Vector3>());

        var canRotate = new AndExpression();

        entity.AddCanRotate(canRotate);

        entity.AddBehaviour(new RotateByMouseBehaviour());
    }
}
