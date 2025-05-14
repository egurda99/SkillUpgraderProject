/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class AmmoAPI
    {
        ///Keys
        public const int IsAmmoFull = 60; // ReactiveVariable<bool>
        public const int AmountAmmoAfterReload = 61; // ReactiveVariable<int>
        public const int CurrentAmmo = 62; // ReactiveVariable<int>
        public const int MaxAmmo = 63; // ReactiveVariable<int>
        public const int IsAmmoEmpty = 64; // ReactiveVariable<bool>
        public const int AmmoRefilled = 65; // IEvent
        public const int CanRefill = 66; // AndExpression


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsAmmoFull(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsAmmoFull);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsAmmoFull(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsAmmoFull, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsAmmoFull(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsAmmoFull, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsAmmoFull(this IEntity obj) => obj.HasValue(IsAmmoFull);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsAmmoFull(this IEntity obj) => obj.DelValue(IsAmmoFull);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsAmmoFull(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsAmmoFull, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<int> GetAmountAmmoAfterReload(this IEntity obj) => obj.GetValue<ReactiveVariable<int>>(AmountAmmoAfterReload);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAmountAmmoAfterReload(this IEntity obj, out ReactiveVariable<int> value) => obj.TryGetValue(AmountAmmoAfterReload, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAmountAmmoAfterReload(this IEntity obj, ReactiveVariable<int> value) => obj.AddValue(AmountAmmoAfterReload, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAmountAmmoAfterReload(this IEntity obj) => obj.HasValue(AmountAmmoAfterReload);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAmountAmmoAfterReload(this IEntity obj) => obj.DelValue(AmountAmmoAfterReload);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAmountAmmoAfterReload(this IEntity obj, ReactiveVariable<int> value) => obj.SetValue(AmountAmmoAfterReload, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<int> GetCurrentAmmo(this IEntity obj) => obj.GetValue<ReactiveVariable<int>>(CurrentAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCurrentAmmo(this IEntity obj, out ReactiveVariable<int> value) => obj.TryGetValue(CurrentAmmo, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCurrentAmmo(this IEntity obj, ReactiveVariable<int> value) => obj.AddValue(CurrentAmmo, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCurrentAmmo(this IEntity obj) => obj.HasValue(CurrentAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCurrentAmmo(this IEntity obj) => obj.DelValue(CurrentAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCurrentAmmo(this IEntity obj, ReactiveVariable<int> value) => obj.SetValue(CurrentAmmo, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<int> GetMaxAmmo(this IEntity obj) => obj.GetValue<ReactiveVariable<int>>(MaxAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMaxAmmo(this IEntity obj, out ReactiveVariable<int> value) => obj.TryGetValue(MaxAmmo, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMaxAmmo(this IEntity obj, ReactiveVariable<int> value) => obj.AddValue(MaxAmmo, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMaxAmmo(this IEntity obj) => obj.HasValue(MaxAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMaxAmmo(this IEntity obj) => obj.DelValue(MaxAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMaxAmmo(this IEntity obj, ReactiveVariable<int> value) => obj.SetValue(MaxAmmo, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsAmmoEmpty(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsAmmoEmpty);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsAmmoEmpty(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsAmmoEmpty, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsAmmoEmpty(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsAmmoEmpty, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsAmmoEmpty(this IEntity obj) => obj.HasValue(IsAmmoEmpty);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsAmmoEmpty(this IEntity obj) => obj.DelValue(IsAmmoEmpty);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsAmmoEmpty(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsAmmoEmpty, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetAmmoRefilled(this IEntity obj) => obj.GetValue<IEvent>(AmmoRefilled);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAmmoRefilled(this IEntity obj, out IEvent value) => obj.TryGetValue(AmmoRefilled, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAmmoRefilled(this IEntity obj, IEvent value) => obj.AddValue(AmmoRefilled, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAmmoRefilled(this IEntity obj) => obj.HasValue(AmmoRefilled);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAmmoRefilled(this IEntity obj) => obj.DelValue(AmmoRefilled);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAmmoRefilled(this IEntity obj, IEvent value) => obj.SetValue(AmmoRefilled, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AndExpression GetCanRefill(this IEntity obj) => obj.GetValue<AndExpression>(CanRefill);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanRefill(this IEntity obj, out AndExpression value) => obj.TryGetValue(CanRefill, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanRefill(this IEntity obj, AndExpression value) => obj.AddValue(CanRefill, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanRefill(this IEntity obj) => obj.HasValue(CanRefill);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanRefill(this IEntity obj) => obj.DelValue(CanRefill);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanRefill(this IEntity obj, AndExpression value) => obj.SetValue(CanRefill, value);
    }
}
