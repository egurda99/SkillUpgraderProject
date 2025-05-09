using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class MeleeReloadBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private IEvent _reloaded;
    private IEvent _attackEvent;
    private ReactiveVariable<float> _reloadTimer;
    private ReactiveVariable<bool> _reloadEnded;
    private float _currentTime;
    private ReactiveVariable<bool> _needReload;

    public void Init(IEntity entity)
    {
        _reloadTimer = entity.GetReloadTime();
        _reloaded = entity.GetReloaded();
        _reloadEnded = entity.GetReloadEnded();

        _needReload = entity.GetNeedReload();
        _attackEvent = entity.GetAttackEvent();
        _currentTime = _reloadTimer.Value;

        _attackEvent.Subscribe(OnAttackEvent);
    }

    private void OnAttackEvent()
    {
        _needReload.Value = true;
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_needReload.Value)
        {
            _currentTime -= deltaTime;
            _reloadEnded.Value = false;

            if (_currentTime <= 0)
            {
                _reloaded?.Invoke();
                Debug.Log("Reloaded");
                _reloadEnded.Value = true;
                _needReload.Value = false;
                _currentTime = _reloadTimer.Value;
            }
        }
    }


    public void Dispose(IEntity entity)
    {
        _attackEvent.Unsubscribe(OnAttackEvent);
    }
}
