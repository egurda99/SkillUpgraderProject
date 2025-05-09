using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class AutoMeleeAttackAfterReloadMechanic : IEntityInstaller
{
    [SerializeField] private float _distanceToAttack = 1f;
    [SerializeField] private float _attackDamage = 1f;

    [SerializeField] private EntityTriggerDispatcher _entityTriggerDispatcher;


    public void Install(IEntity entity)
    {
        entity.AddAttackDamage(_attackDamage);
        entity.AddNeedReload(new ReactiveVariable<bool>(true));
        entity.AddReloadEnded(new ReactiveVariable<bool>());
        entity.AddDistanceToAttack(_distanceToAttack);

        entity.AddCanAttack(new AndExpression());
        entity.AddIsAttacking(new ReactiveVariable<bool>());

        entity.AddAttackRequest(new BaseEvent());
        entity.AddAttackAction(new BaseEvent());
        entity.AddAttackEvent(new BaseEvent());

        entity.AddEntityTriggerDispatcher(_entityTriggerDispatcher);


        entity.AddBehaviour(new AutoMeleeAttackAfterReloadBehaviour());
    }
}
