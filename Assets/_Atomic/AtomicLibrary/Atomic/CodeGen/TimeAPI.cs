/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class TimeAPI
    {
        ///Keys
        public const int ReloadTime = 24; // ReactiveVariable<float>
        public const int ReloadEnded = 25; // ReactiveVariable<bool>
        public const int LifeTime = 29; // ReactiveVariable<float>
        public const int AmmoRefillTime = 40; // ReactiveVariable<float>
        public const int CanStartTimer = 47; // AndExpression


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetReloadTime(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(ReloadTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetReloadTime(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(ReloadTime, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddReloadTime(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(ReloadTime, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasReloadTime(this IEntity obj) => obj.HasValue(ReloadTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelReloadTime(this IEntity obj) => obj.DelValue(ReloadTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetReloadTime(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(ReloadTime, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetReloadEnded(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(ReloadEnded);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetReloadEnded(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(ReloadEnded, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddReloadEnded(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(ReloadEnded, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasReloadEnded(this IEntity obj) => obj.HasValue(ReloadEnded);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelReloadEnded(this IEntity obj) => obj.DelValue(ReloadEnded);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetReloadEnded(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(ReloadEnded, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetLifeTime(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(LifeTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetLifeTime(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(LifeTime, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddLifeTime(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(LifeTime, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasLifeTime(this IEntity obj) => obj.HasValue(LifeTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelLifeTime(this IEntity obj) => obj.DelValue(LifeTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLifeTime(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(LifeTime, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetAmmoRefillTime(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(AmmoRefillTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAmmoRefillTime(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(AmmoRefillTime, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAmmoRefillTime(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(AmmoRefillTime, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAmmoRefillTime(this IEntity obj) => obj.HasValue(AmmoRefillTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAmmoRefillTime(this IEntity obj) => obj.DelValue(AmmoRefillTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAmmoRefillTime(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(AmmoRefillTime, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AndExpression GetCanStartTimer(this IEntity obj) => obj.GetValue<AndExpression>(CanStartTimer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanStartTimer(this IEntity obj, out AndExpression value) => obj.TryGetValue(CanStartTimer, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanStartTimer(this IEntity obj, AndExpression value) => obj.AddValue(CanStartTimer, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanStartTimer(this IEntity obj) => obj.HasValue(CanStartTimer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanStartTimer(this IEntity obj) => obj.DelValue(CanStartTimer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanStartTimer(this IEntity obj, AndExpression value) => obj.SetValue(CanStartTimer, value);
    }
}
