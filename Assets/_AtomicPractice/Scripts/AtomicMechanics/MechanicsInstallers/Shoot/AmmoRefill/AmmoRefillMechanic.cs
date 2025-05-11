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
        entity.AddAmmoRefilled(new BaseEvent());
        entity.AddCanRefill(new AndExpression());

        entity.AddBehaviour(new AmmoRefillBehaviour());
    }
}
