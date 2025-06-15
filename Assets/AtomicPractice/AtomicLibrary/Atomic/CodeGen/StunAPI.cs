/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class StunAPI
    {
        ///Keys
        public const int IsStunned = 59; // ReactiveVariable<bool>
        public const int StunFX = 57; // ParticleSystem


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsStunned(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsStunned);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsStunned(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsStunned, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsStunned(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsStunned, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsStunned(this IEntity obj) => obj.HasValue(IsStunned);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsStunned(this IEntity obj) => obj.DelValue(IsStunned);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsStunned(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsStunned, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParticleSystem GetStunFX(this IEntity obj) => obj.GetValue<ParticleSystem>(StunFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetStunFX(this IEntity obj, out ParticleSystem value) => obj.TryGetValue(StunFX, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddStunFX(this IEntity obj, ParticleSystem value) => obj.AddValue(StunFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasStunFX(this IEntity obj) => obj.HasValue(StunFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelStunFX(this IEntity obj) => obj.DelValue(StunFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetStunFX(this IEntity obj, ParticleSystem value) => obj.SetValue(StunFX, value);
    }
}
