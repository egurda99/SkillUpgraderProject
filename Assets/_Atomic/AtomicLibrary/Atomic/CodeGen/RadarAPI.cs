/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class RadarAPI
    {
        ///Keys
        public const int Radius = 21; // ReactiveVariable<float>
        public const int IsFound = 22; // ReactiveVariable<bool>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetRadius(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(Radius);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRadius(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(Radius, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRadius(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(Radius, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRadius(this IEntity obj) => obj.HasValue(Radius);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRadius(this IEntity obj) => obj.DelValue(Radius);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRadius(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(Radius, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsFound(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsFound);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsFound(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsFound, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsFound(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsFound, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsFound(this IEntity obj) => obj.HasValue(IsFound);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsFound(this IEntity obj) => obj.DelValue(IsFound);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsFound(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsFound, value);
    }
}
