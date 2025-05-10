using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class TakeDamageVFXMechanic : IEntityInstaller
{
    [SerializeField] private ParticleSystem _takeDamageFX;


    public void Install(IEntity entity)
    {
        entity.AddTakeDamageFX(_takeDamageFX);

        entity.AddBehaviour(new TakeDamageVFXBehaviour());
    }
}
