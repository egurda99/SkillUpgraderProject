using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class AmmoMechanic : IEntityInstaller
{
    [SerializeField] private int _startAmmo;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _ammoAfterReload;


    public void Install(IEntity entity)
    {
        entity.AddCurrentAmmo(_startAmmo);
        entity.AddMaxAmmo(_maxAmmo);
        entity.AddAmountAmmoAfterReload(_ammoAfterReload);
        entity.AddIsAmmoEmpty(new ReactiveVariable<bool>());

        entity.AddIsAmmoFull(new ReactiveVariable<bool>(false));

        entity.AddBehaviour(new AmmoBehaviour());
    }
}
