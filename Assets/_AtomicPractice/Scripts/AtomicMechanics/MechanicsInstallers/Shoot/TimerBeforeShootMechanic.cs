using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class TimerBeforeShootMechanic : IEntityInstaller
{
    [SerializeField] private float _reloadTime;

    public void Install(IEntity entity)
    {
        entity.AddReloadTime(_reloadTime);
        entity.AddReloadEnded(new ReactiveVariable<bool>());
        entity.AddBehaviour(new TimerBeforeShootBehaviour());
    }
}