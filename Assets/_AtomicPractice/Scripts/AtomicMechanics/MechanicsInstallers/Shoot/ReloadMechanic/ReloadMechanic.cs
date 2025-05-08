using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class ReloadMechanic : IEntityInstaller
{
    [SerializeField] private float _reloadTime;

    public void Install(IEntity entity)
    {
        entity.AddReloadTime(_reloadTime);
        entity.AddReloadEnded(new ReactiveVariable<bool>());
        entity.AddReloaded(new BaseEvent());

        entity.AddBehaviour(new ReloadMechanicBehaviour());
    }
}
