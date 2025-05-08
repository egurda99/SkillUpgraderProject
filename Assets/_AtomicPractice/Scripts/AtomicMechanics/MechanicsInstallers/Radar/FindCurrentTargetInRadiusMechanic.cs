using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class FindCurrentTargetInRadiusMechanic : IEntityInstaller
{
    [SerializeField] private float _radius;
    [SerializeField] private Transform _root;
    [SerializeField] private Transform _target;


    public void Install(IEntity entity)
    {
        entity.AddRootTransform(_root);
        entity.AddRadius(_radius);
        entity.AddTarget(_target);

        entity.AddIsFound(new ReactiveVariable<bool>());
        entity.AddBehaviour(new FindCurrentTargetInRadiusBehaviour());
    }
}
