using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class StunMechanic : IEntityInstaller
{
    [SerializeField] private float _stunTime;

    public void Install(IEntity entity)
    {
        entity.AddStunTime(_stunTime);
        entity.AddIsStunned(new ReactiveVariable<bool>());
        entity.AddStunTimer(new Timer());

        entity.AddBehaviour(new StunBehaviour());
    }
}
