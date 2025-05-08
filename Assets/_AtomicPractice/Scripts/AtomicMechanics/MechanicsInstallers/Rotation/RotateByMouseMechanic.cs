using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public class RotateByMouseMechanic : IEntityInstaller
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private float _rotationSpeed = 100f;

    public void Install(IEntity entity)
    {
        entity.AddRootTransform(_rootTransform);
        entity.AddRotationSpeed(_rotationSpeed);
        entity.AddMouseDeltaX(new ReactiveVariable<float>());

        entity.AddBehaviour(new RotateByMouseBehaviour());
    }
}