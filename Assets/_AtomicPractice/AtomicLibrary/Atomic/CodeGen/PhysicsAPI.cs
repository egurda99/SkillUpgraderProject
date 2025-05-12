/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class PhysicsAPI
    {
        ///Keys
        public const int TriggerEventDispatcher = 52; // TriggerEventDispatcher


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TriggerEventDispatcher GetTriggerEventDispatcher(this IEntity obj) => obj.GetValue<TriggerEventDispatcher>(TriggerEventDispatcher);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTriggerEventDispatcher(this IEntity obj, out TriggerEventDispatcher value) => obj.TryGetValue(TriggerEventDispatcher, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTriggerEventDispatcher(this IEntity obj, TriggerEventDispatcher value) => obj.AddValue(TriggerEventDispatcher, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTriggerEventDispatcher(this IEntity obj) => obj.HasValue(TriggerEventDispatcher);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTriggerEventDispatcher(this IEntity obj) => obj.DelValue(TriggerEventDispatcher);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTriggerEventDispatcher(this IEntity obj, TriggerEventDispatcher value) => obj.SetValue(TriggerEventDispatcher, value);
    }
}
