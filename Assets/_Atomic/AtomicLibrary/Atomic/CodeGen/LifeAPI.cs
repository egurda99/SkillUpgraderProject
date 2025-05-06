/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class LifeAPI
    {
        ///Keys
        public const int IsDead = 14; // ReactiveVariable<bool>
        public const int HitPoints = 15; // ReactiveVariable<float>
        public const int TakeDamageAction = 16; // IEvent<float>
        public const int HealAction = 17; // IEvent<float>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsDead(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsDead);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsDead(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsDead, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsDead(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsDead, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsDead(this IEntity obj) => obj.HasValue(IsDead);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsDead(this IEntity obj) => obj.DelValue(IsDead);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsDead(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsDead, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetHitPoints(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(HitPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetHitPoints(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(HitPoints, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddHitPoints(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(HitPoints, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasHitPoints(this IEntity obj) => obj.HasValue(HitPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelHitPoints(this IEntity obj) => obj.DelValue(HitPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHitPoints(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(HitPoints, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent<float> GetTakeDamageAction(this IEntity obj) => obj.GetValue<IEvent<float>>(TakeDamageAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTakeDamageAction(this IEntity obj, out IEvent<float> value) => obj.TryGetValue(TakeDamageAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTakeDamageAction(this IEntity obj, IEvent<float> value) => obj.AddValue(TakeDamageAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTakeDamageAction(this IEntity obj) => obj.HasValue(TakeDamageAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTakeDamageAction(this IEntity obj) => obj.DelValue(TakeDamageAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTakeDamageAction(this IEntity obj, IEvent<float> value) => obj.SetValue(TakeDamageAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent<float> GetHealAction(this IEntity obj) => obj.GetValue<IEvent<float>>(HealAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetHealAction(this IEntity obj, out IEvent<float> value) => obj.TryGetValue(HealAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddHealAction(this IEntity obj, IEvent<float> value) => obj.AddValue(HealAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasHealAction(this IEntity obj) => obj.HasValue(HealAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelHealAction(this IEntity obj) => obj.DelValue(HealAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHealAction(this IEntity obj, IEvent<float> value) => obj.SetValue(HealAction, value);
    }
}
