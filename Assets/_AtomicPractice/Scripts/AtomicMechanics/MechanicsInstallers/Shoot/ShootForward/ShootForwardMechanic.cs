using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class ShootForwardMechanic : IEntityInstaller
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private bool _isShooting;
    [SerializeField] private bool _canShoot = true;


    public void Install(IEntity entity)
    {
        entity.AddFirePoint(_firePoint);
        entity.AddBulletPrefab(_bulletPrefab);

        entity.AddShootRequest(new BaseEvent());
        entity.AddShootAction(new BaseEvent());
        entity.AddShootEvent(new BaseEvent());

        entity.AddIsShooting(_isShooting);

        var canShoot = new AndExpression();
        entity.AddCanShoot(canShoot);

        entity.AddBehaviour(new ShootForwardBehaviour());
    }
}
