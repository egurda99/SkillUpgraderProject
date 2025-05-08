using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class TimerBeforeShootBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private ReactiveVariable<float> _reloadTime;
    private ReactiveVariable<bool> _reloadEnded;
    private float _currentTime;
    private IEvent _shootEvent;

    private AndExpression _canShoot;
    private IEvent _shootAction;

    public void Init(IEntity entity)
    {
        _reloadTime = entity.GetReloadTime();
        _reloadEnded = entity.GetReloadEnded();
        _reloadEnded.Value = true;
        _currentTime = _reloadTime.Value;

        _canShoot = entity.GetCanShoot();
        _shootEvent = entity.GetShootEvent();
        _shootAction = entity.GetShootAction();

        _shootEvent.Subscribe(OnShoot);
    }

    private void OnShoot()
    {
        _reloadEnded.Value = false;
        _currentTime = _reloadTime.Value;
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_reloadEnded.Value == false)
        {
            _currentTime -= deltaTime;
            if (_currentTime <= 0)
            {
                _reloadEnded.Value = true;
                _currentTime = 0;
            }
        }

        if (_canShoot.Value)
        {
            _shootAction?.Invoke();
        }
    }

    public void Dispose(IEntity entity)
    {
        _shootEvent.Unsubscribe(OnShoot);
    }
}
