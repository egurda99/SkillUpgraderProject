using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class ShootForwardBehaviour : IEntityInit, IEntityDispose
{
    private IEvent _shootAction;
    private IEvent _shootEvent;
    private IEvent _shootRequested;

    private GameObject _bulletPrefab;
    private Transform _firePoint;
    private ReactiveVariable<bool> _isShooting;
    private AndExpression _canShoot;
    private Transform _rootTransform;

    public void Init(IEntity entity)
    {
        _shootAction = entity.GetShootAction();
        _shootEvent = entity.GetShootEvent();
        _shootRequested = entity.GetShootRequest();

        _bulletPrefab = entity.GetBulletPrefab();

        _firePoint = entity.GetFirePoint();
        _rootTransform = entity.GetRootTransform();
        _isShooting = entity.GetIsShooting();
        _canShoot = entity.GetCanShoot();

        _shootAction.Subscribe(ShootAction);

        _shootRequested.Subscribe(OnShootRequested);
    }

    private void OnShootRequested()
    {
        if (_canShoot.Value)
        {
            _isShooting.Value = true;
        }
    }

    private void ShootAction()
    {
        if (_canShoot.Value)
        {
            var bulletGO = Object.Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);

            var bulletEntity = bulletGO.GetComponent<SceneEntity>();
            bulletEntity.GetMoveDirection().Value = _rootTransform.forward;
            _shootEvent?.Invoke();
        }

        _isShooting.Value = false;
    }

    public void Dispose(IEntity entity)
    {
        _shootAction.Unsubscribe(ShootAction);
        _shootRequested.Unsubscribe(OnShootRequested);
    }
}
