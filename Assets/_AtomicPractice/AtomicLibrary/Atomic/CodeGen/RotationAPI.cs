/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class RotationAPI
    {
        ///Keys
        public const int RotationSpeed = 18; // ReactiveVariable<float>
        public const int CanRotate = 19; // AndExpression
        public const int IsRotating = 20; // ReactiveVariable<bool>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<float> GetRotationSpeed(this IEntity obj) => obj.GetValue<ReactiveVariable<float>>(RotationSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRotationSpeed(this IEntity obj, out ReactiveVariable<float> value) => obj.TryGetValue(RotationSpeed, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRotationSpeed(this IEntity obj, ReactiveVariable<float> value) => obj.AddValue(RotationSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRotationSpeed(this IEntity obj) => obj.HasValue(RotationSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRotationSpeed(this IEntity obj) => obj.DelValue(RotationSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotationSpeed(this IEntity obj, ReactiveVariable<float> value) => obj.SetValue(RotationSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AndExpression GetCanRotate(this IEntity obj) => obj.GetValue<AndExpression>(CanRotate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanRotate(this IEntity obj, out AndExpression value) => obj.TryGetValue(CanRotate, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanRotate(this IEntity obj, AndExpression value) => obj.AddValue(CanRotate, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanRotate(this IEntity obj) => obj.HasValue(CanRotate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanRotate(this IEntity obj) => obj.DelValue(CanRotate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanRotate(this IEntity obj, AndExpression value) => obj.SetValue(CanRotate, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsRotating(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsRotating);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsRotating(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsRotating, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsRotating(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsRotating, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsRotating(this IEntity obj) => obj.HasValue(IsRotating);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsRotating(this IEntity obj) => obj.DelValue(IsRotating);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsRotating(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsRotating, value);
    }
}
