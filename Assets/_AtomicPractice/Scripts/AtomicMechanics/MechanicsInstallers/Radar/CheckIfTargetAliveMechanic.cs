using System;
using Atomic.Elements;
using Atomic.Entities;

[Serializable]
public sealed class CheckIfTargetAliveMechanic : IEntityInstaller
{
    public void Install(IEntity entity)
    {
        entity.AddIsTargetAlive(new ReactiveVariable<bool>());

        entity.AddBehaviour(new CheckIfTargetAliveBehaviour());
    }
}
