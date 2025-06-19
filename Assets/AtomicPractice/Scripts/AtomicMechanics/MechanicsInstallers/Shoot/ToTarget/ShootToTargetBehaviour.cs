using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class ShootToTargetBehaviour : IEntityInit, IEntityDispose
{
    private IEvent _shootAction;
    private IEvent _shootEvent;
    private IEvent _shootRequested;

    private IEvent<Transform> _changeTargetAction;

    private GameObject _bulletPrefab;
    private ReactiveVariable<Transform> _target;
    private Transform _firePoint;
    private ReactiveVariable<bool> _isShooting;
    private AndExpression _canShoot;

    public void Init(IEntity entity)
    {
        _shootAction = entity.GetShootAction();
        _shootEvent = entity.GetShootEvent();
        _shootRequested = entity.GetShootRequest();


        _bulletPrefab = entity.GetBulletPrefab();
        _target = entity.GetTarget();
        _firePoint = entity.GetFirePoint();
        _isShooting = entity.GetIsShooting();
        _canShoot = entity.GetCanShoot();
        _changeTargetAction = entity.GetChangeTargetAction();

        _shootAction.Subscribe(ShootAction);
        _changeTargetAction.Subscribe(OnTargetChanged);
        _shootRequested.Subscribe(OnShootRequested);
    }

    private void OnShootRequested()
    {
        _isShooting.Value = true;
    }

    private void OnTargetChanged(Transform target)
    {
        _target.Value = target;
    }

    private void ShootAction()
    {
        if (_canShoot.Value)
        {
            var bulletGO = Object.Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);

            var bulletEntity = bulletGO.GetComponent<SceneEntity>();

            Debug.Log("Shooted");

            bulletEntity.GetMoveDirection().Value = (_target.Value.position - _firePoint.position).normalized;
            _shootEvent?.Invoke();
        }

        _isShooting.Value = false;
    }

    public void Dispose(IEntity entity)
    {
        _shootAction.Unsubscribe(ShootAction);
        _changeTargetAction.Unsubscribe(OnTargetChanged);
        _shootRequested.Unsubscribe(OnShootRequested);
    }
}
