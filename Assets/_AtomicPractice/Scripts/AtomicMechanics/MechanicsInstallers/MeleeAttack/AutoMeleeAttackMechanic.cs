using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class AutoMeleeAttackMechanic : IEntityInstaller
{
    [SerializeField] private float _distanceToAttack = 1f;
    [SerializeField] private float _attackDamage = 1f;

    public void Install(IEntity entity)
    {
        entity.AddAttackDamage(_attackDamage);
        entity.AddDistanceToAttack(_distanceToAttack);

        entity.AddCanAttack(new AndExpression());
        entity.AddIsAttacking(new ReactiveVariable<bool>());

        entity.AddAttackRequest(new BaseEvent());
        entity.AddAttackAction(new BaseEvent());
        entity.AddAttackEvent(new BaseEvent());


        entity.AddBehaviour(new AutoMeleeAttackBehaviour());
    }
}
