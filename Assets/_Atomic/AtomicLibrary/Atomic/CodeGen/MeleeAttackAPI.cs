/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class MeleeAttackAPI
    {
        ///Keys
        public const int DistanceToAttack = 41; // ReactiveVariable<float>
        public const int CanAttack = 42; // AndExpression
        public const int AttackRequest = 44; // IEvent
        public const int AttackAction = 45; // IEvent
        public const int AttackEvent = 46; // IEvent
        public const int AttackDamage = 49; // ReactiveVariable<float>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetDistanceToAttack(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(DistanceToAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDistanceToAttack(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(DistanceToAttack, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDistanceToAttack(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(DistanceToAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasDistanceToAttack(this IEntity obj) => obj.HasValue(DistanceToAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDistanceToAttack(this IEntity obj) => obj.DelValue(DistanceToAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDistanceToAttack(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(DistanceToAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AndExpression GetCanAttack(this IEntity obj) => obj.GetValue<AndExpression>(CanAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanAttack(this IEntity obj, out AndExpression value) => obj.TryGetValue(CanAttack, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanAttack(this IEntity obj, AndExpression value) => obj.AddValue(CanAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanAttack(this IEntity obj) => obj.HasValue(CanAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanAttack(this IEntity obj) => obj.DelValue(CanAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanAttack(this IEntity obj, AndExpression value) => obj.SetValue(CanAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetAttackRequest(this IEntity obj) => obj.GetValue<IEvent>(AttackRequest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackRequest(this IEntity obj, out IEvent value) => obj.TryGetValue(AttackRequest, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackRequest(this IEntity obj, IEvent value) => obj.AddValue(AttackRequest, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackRequest(this IEntity obj) => obj.HasValue(AttackRequest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackRequest(this IEntity obj) => obj.DelValue(AttackRequest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackRequest(this IEntity obj, IEvent value) => obj.SetValue(AttackRequest, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetAttackAction(this IEntity obj) => obj.GetValue<IEvent>(AttackAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackAction(this IEntity obj, out IEvent value) => obj.TryGetValue(AttackAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackAction(this IEntity obj, IEvent value) => obj.AddValue(AttackAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackAction(this IEntity obj) => obj.HasValue(AttackAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackAction(this IEntity obj) => obj.DelValue(AttackAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackAction(this IEntity obj, IEvent value) => obj.SetValue(AttackAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetAttackEvent(this IEntity obj) => obj.GetValue<IEvent>(AttackEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackEvent(this IEntity obj, out IEvent value) => obj.TryGetValue(AttackEvent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackEvent(this IEntity obj, IEvent value) => obj.AddValue(AttackEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackEvent(this IEntity obj) => obj.HasValue(AttackEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackEvent(this IEntity obj) => obj.DelValue(AttackEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackEvent(this IEntity obj, IEvent value) => obj.SetValue(AttackEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetAttackDamage(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(AttackDamage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackDamage(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(AttackDamage, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackDamage(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(AttackDamage, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackDamage(this IEntity obj) => obj.HasValue(AttackDamage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackDamage(this IEntity obj) => obj.DelValue(AttackDamage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackDamage(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(AttackDamage, value);
    }
}
