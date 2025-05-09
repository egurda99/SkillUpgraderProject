using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class AutoShootMechanic : IEntityInstaller
{
    [SerializeField] private float _reloadTime;

    public void Install(IEntity entity)
    {
        entity.AddReloadTime(_reloadTime);

        entity.AddBehaviour(new AutoShootBehaviour());
    }
}
