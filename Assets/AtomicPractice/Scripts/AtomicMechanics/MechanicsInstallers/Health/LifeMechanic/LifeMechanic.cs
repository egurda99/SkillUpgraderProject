using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public class LifeMechanic : IEntityInstaller
{
    [SerializeField] private float _hitPoints = 5f;
    [SerializeField] private bool _isDead;


    public void Install(IEntity entity)
    {
        entity.AddHitPoints(_hitPoints);
        entity.AddIsDead(_isDead);

        entity.AddTakeDamageAction(new BaseEvent<float>());
        entity.AddHealAction(new BaseEvent<float>());

        entity.AddBehaviour(new LifeBehaviour());
    }
}
