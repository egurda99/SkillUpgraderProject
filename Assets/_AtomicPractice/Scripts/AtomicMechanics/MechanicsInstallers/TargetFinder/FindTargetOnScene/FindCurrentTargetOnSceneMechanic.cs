using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class FindCurrentTargetOnSceneMechanic : IEntityInstaller
{
    [SerializeField] private Transform _target;


    public void Install(IEntity entity)
    {
        entity.AddTarget(_target);
        entity.AddIsFound(new ReactiveVariable<bool>());
        entity.AddBehaviour(new FindCurrentTargetOnSceneBehaviour());
    }
}
