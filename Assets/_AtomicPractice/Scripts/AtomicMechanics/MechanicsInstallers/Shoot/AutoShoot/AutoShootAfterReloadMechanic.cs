using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class AutoShootAfterReloadShootMechanic : IEntityInstaller
{
    [SerializeField] private float _reloadTime;

    public void Install(IEntity entity)
    {
        entity.AddReloadTime(_reloadTime);
        entity.AddNeedReload(new ReactiveVariable<bool>(true));
        entity.AddReloadEnded(new ReactiveVariable<bool>());
        entity.AddBehaviour(new AutoShootAfterReloadShootBehaviour());
    }
}
