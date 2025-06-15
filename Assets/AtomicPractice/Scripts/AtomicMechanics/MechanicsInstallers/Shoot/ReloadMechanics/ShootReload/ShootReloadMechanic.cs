using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class ShootReloadMechanic : IEntityInstaller
{
    [SerializeField] private float _reloadTime;

    public void Install(IEntity entity)
    {
        entity.AddReloadTime(_reloadTime);
        entity.AddNeedReload(new ReactiveVariable<bool>(true));
        entity.AddReloadEnded(new ReactiveVariable<bool>());
        entity.AddReloaded(new BaseEvent());
        entity.AddReloadTimer(new Timer());


        entity.AddBehaviour(new ShootReloadBehaviour());
    }
}
