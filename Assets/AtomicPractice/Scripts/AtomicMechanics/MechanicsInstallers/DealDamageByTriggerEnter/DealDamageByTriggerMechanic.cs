using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class DealDamageByTriggerMechanic : IEntityInstaller
{
    [SerializeField] private TriggerEventDispatcher _triggerEventDispatcher;
    [SerializeField] private float _damage;


    public void Install(IEntity entity)
    {
        entity.AddTriggerEventDispatcher(_triggerEventDispatcher);
        entity.AddAttackDamage(_damage);

        entity.AddBehaviour(new DealDamageByTriggerBehaviour());
    }
}
