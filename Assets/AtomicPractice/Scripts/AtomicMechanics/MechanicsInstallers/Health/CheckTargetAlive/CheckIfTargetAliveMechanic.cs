using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class CheckIfTargetAliveMechanic : IEntityInstaller
{
    public void Install(IEntity entity)
    {
        entity.AddIsTargetAlive(new ReactiveVariable<bool>());
        entity.AddChangeTargetAction(new BaseEvent<Transform>());

        entity.AddBehaviour(new CheckIfTargetAliveBehaviour());
    }
}
