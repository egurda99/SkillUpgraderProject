/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class IDAPI
    {
        ///Keys
        public const int EntityID = 32; // ReactiveVariable<string>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<string> GetEntityID(this IEntity obj) => obj.GetValue<ReactiveVariable<string>>(EntityID);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntityID(this IEntity obj, out ReactiveVariable<string> value) => obj.TryGetValue(EntityID, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddEntityID(this IEntity obj, ReactiveVariable<string> value) => obj.AddValue(EntityID, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasEntityID(this IEntity obj) => obj.HasValue(EntityID);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelEntityID(this IEntity obj) => obj.DelValue(EntityID);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEntityID(this IEntity obj, ReactiveVariable<string> value) => obj.SetValue(EntityID, value);
    }
}
