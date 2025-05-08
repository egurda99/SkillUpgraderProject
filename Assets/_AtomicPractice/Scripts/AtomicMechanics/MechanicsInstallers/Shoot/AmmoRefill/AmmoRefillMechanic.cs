using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class AmmoRefillMechanic : IEntityInstaller
{
    [SerializeField] private float _ammoRefillTime;

    public void Install(IEntity entity)
    {
        entity.AddAmmoRefillTime(_ammoRefillTime);
        entity.AddAmmoAdded(new BaseEvent());

        entity.AddBehaviour(new AmmoRefillBehaviour());
    }
}
