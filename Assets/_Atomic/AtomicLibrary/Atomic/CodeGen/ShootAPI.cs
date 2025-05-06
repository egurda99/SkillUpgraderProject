/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Entities
{
    public static class ShootAPI
    {
        ///Keys
        public const int ShootAction = 1; // IEvent
        public const int ShootRequest = 7; // IEvent
        public const int ShootEvent = 8; // IEvent
        public const int IsShooting = 9; // ReactiveVariable<bool>
        public const int CanShoot = 10; // AndExpression
        public const int BulletPrefab = 11; // GameObject
        public const int FirePoint = 12; // Transform
        public const int Target = 13; // ReactiveVariable<Transform>
        public const int ChangeTargetAction = 23; // IEvent<Transform>
        public const int IsTargetAlive = 28; // ReactiveVariable<bool>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetShootAction(this IEntity obj) => obj.GetValue<IEvent>(ShootAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetShootAction(this IEntity obj, out IEvent value) => obj.TryGetValue(ShootAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddShootAction(this IEntity obj, IEvent value) => obj.AddValue(ShootAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasShootAction(this IEntity obj) => obj.HasValue(ShootAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelShootAction(this IEntity obj) => obj.DelValue(ShootAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShootAction(this IEntity obj, IEvent value) => obj.SetValue(ShootAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetShootRequest(this IEntity obj) => obj.GetValue<IEvent>(ShootRequest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetShootRequest(this IEntity obj, out IEvent value) => obj.TryGetValue(ShootRequest, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddShootRequest(this IEntity obj, IEvent value) => obj.AddValue(ShootRequest, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasShootRequest(this IEntity obj) => obj.HasValue(ShootRequest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelShootRequest(this IEntity obj) => obj.DelValue(ShootRequest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShootRequest(this IEntity obj, IEvent value) => obj.SetValue(ShootRequest, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent GetShootEvent(this IEntity obj) => obj.GetValue<IEvent>(ShootEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetShootEvent(this IEntity obj, out IEvent value) => obj.TryGetValue(ShootEvent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddShootEvent(this IEntity obj, IEvent value) => obj.AddValue(ShootEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasShootEvent(this IEntity obj) => obj.HasValue(ShootEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelShootEvent(this IEntity obj) => obj.DelValue(ShootEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShootEvent(this IEntity obj, IEvent value) => obj.SetValue(ShootEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsShooting(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsShooting);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsShooting(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsShooting, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsShooting(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsShooting, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsShooting(this IEntity obj) => obj.HasValue(IsShooting);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsShooting(this IEntity obj) => obj.DelValue(IsShooting);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsShooting(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsShooting, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AndExpression GetCanShoot(this IEntity obj) => obj.GetValue<AndExpression>(CanShoot);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanShoot(this IEntity obj, out AndExpression value) => obj.TryGetValue(CanShoot, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanShoot(this IEntity obj, AndExpression value) => obj.AddValue(CanShoot, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanShoot(this IEntity obj) => obj.HasValue(CanShoot);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanShoot(this IEntity obj) => obj.DelValue(CanShoot);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanShoot(this IEntity obj, AndExpression value) => obj.SetValue(CanShoot, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject GetBulletPrefab(this IEntity obj) => obj.GetValue<GameObject>(BulletPrefab);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetBulletPrefab(this IEntity obj, out GameObject value) => obj.TryGetValue(BulletPrefab, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddBulletPrefab(this IEntity obj, GameObject value) => obj.AddValue(BulletPrefab, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasBulletPrefab(this IEntity obj) => obj.HasValue(BulletPrefab);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelBulletPrefab(this IEntity obj) => obj.DelValue(BulletPrefab);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBulletPrefab(this IEntity obj, GameObject value) => obj.SetValue(BulletPrefab, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetFirePoint(this IEntity obj) => obj.GetValue<Transform>(FirePoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFirePoint(this IEntity obj, out Transform value) => obj.TryGetValue(FirePoint, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddFirePoint(this IEntity obj, Transform value) => obj.AddValue(FirePoint, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFirePoint(this IEntity obj) => obj.HasValue(FirePoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelFirePoint(this IEntity obj) => obj.DelValue(FirePoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFirePoint(this IEntity obj, Transform value) => obj.SetValue(FirePoint, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<Transform> GetTarget(this IEntity obj) => obj.GetValue<ReactiveVariable<Transform>>(Target);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTarget(this IEntity obj, out ReactiveVariable<Transform> value) => obj.TryGetValue(Target, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTarget(this IEntity obj, ReactiveVariable<Transform> value) => obj.AddValue(Target, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTarget(this IEntity obj) => obj.HasValue(Target);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTarget(this IEntity obj) => obj.DelValue(Target);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTarget(this IEntity obj, ReactiveVariable<Transform> value) => obj.SetValue(Target, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEvent<Transform> GetChangeTargetAction(this IEntity obj) => obj.GetValue<IEvent<Transform>>(ChangeTargetAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetChangeTargetAction(this IEntity obj, out IEvent<Transform> value) => obj.TryGetValue(ChangeTargetAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddChangeTargetAction(this IEntity obj, IEvent<Transform> value) => obj.AddValue(ChangeTargetAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasChangeTargetAction(this IEntity obj) => obj.HasValue(ChangeTargetAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelChangeTargetAction(this IEntity obj) => obj.DelValue(ChangeTargetAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetChangeTargetAction(this IEntity obj, IEvent<Transform> value) => obj.SetValue(ChangeTargetAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetIsTargetAlive(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(IsTargetAlive);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsTargetAlive(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(IsTargetAlive, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsTargetAlive(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(IsTargetAlive, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsTargetAlive(this IEntity obj) => obj.HasValue(IsTargetAlive);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsTargetAlive(this IEntity obj) => obj.DelValue(IsTargetAlive);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsTargetAlive(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(IsTargetAlive, value);
    }
}
