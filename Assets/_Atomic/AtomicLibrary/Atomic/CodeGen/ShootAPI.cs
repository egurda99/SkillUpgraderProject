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
        public const int IsAmmoFull = 32; // ReactiveVariable<bool>
        public const int Reloaded = 33; // IEvent
        public const int NeedReload = 34; // ReactiveVariable<bool>
        public const int AmountAmmoAfterReload = 35; // ReactiveVariable<int>
        public const int CurrentAmmo = 36; // ReactiveVariable<int>
        public const int MaxAmmo = 37; // ReactiveVariable<int>
        public const int IsAmmoEmpty = 38; // ReactiveVariable<bool>


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
        public static IEvent GetReloaded(this IEntity obj) => obj.GetValue<IEvent>(Reloaded);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetReloaded(this IEntity obj, out IEvent value) => obj.TryGetValue(Reloaded, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddReloaded(this IEntity obj, IEvent value) => obj.AddValue(Reloaded, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasReloaded(this IEntity obj) => obj.HasValue(Reloaded);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelReloaded(this IEntity obj) => obj.DelValue(Reloaded);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetReloaded(this IEntity obj, IEvent value) => obj.SetValue(Reloaded, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<bool> GetNeedReload(this IEntity obj) => obj.GetValue<ReactiveVariable<bool>>(NeedReload);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetNeedReload(this IEntity obj, out ReactiveVariable<bool> value) => obj.TryGetValue(NeedReload, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddNeedReload(this IEntity obj, ReactiveVariable<bool> value) => obj.AddValue(NeedReload, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNeedReload(this IEntity obj) => obj.HasValue(NeedReload);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelNeedReload(this IEntity obj) => obj.DelValue(NeedReload);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetNeedReload(this IEntity obj, ReactiveVariable<bool> value) => obj.SetValue(NeedReload, value);

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
    }
}
