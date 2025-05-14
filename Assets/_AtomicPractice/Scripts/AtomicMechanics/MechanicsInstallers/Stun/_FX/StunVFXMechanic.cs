using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class StunVFXMechanic : IEntityInstaller
{
    [SerializeField] private ParticleSystem _stunFX;

    public void Install(IEntity entity)
    {
        entity.AddStunFX(_stunFX);

        entity.AddBehaviour(new StunVFXBehaviour());
    }
}
