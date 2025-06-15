using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public class DestroyByLifeTimeMechanic : IEntityInstaller
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private Transform _rootTransform;


    public void Install(IEntity entity)
    {
        entity.AddLifeTime(_lifeTime);
        entity.AddRootTransform(_rootTransform);
        entity.AddCanStartTimer(new AndExpression());
        entity.AddLifetimeTimer(new Timer());


        entity.AddBehaviour(new DestroyByLifeTimeBehaviour());
    }
}
