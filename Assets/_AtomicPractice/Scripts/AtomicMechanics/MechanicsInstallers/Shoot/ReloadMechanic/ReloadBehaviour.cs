using Atomic.Elements;
using Atomic.Entities;

public sealed class ReloadBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private IEvent _reloaded;
    private IEvent _shootEvent;
    private ReactiveVariable<float> _reloadTimer;
    private ReactiveVariable<bool> _reloadEnded;
    private float _currentTime;

    public void Init(IEntity entity)
    {
        _reloadTimer = entity.GetReloadTime();
        _reloaded = entity.GetReloaded();
        _reloadEnded = entity.GetReloadEnded();

        _shootEvent = entity.GetShootEvent();
        _currentTime = _reloadTimer.Value;

        _shootEvent.Subscribe(OnShootEvent);
    }

    private void OnShootEvent()
    {
        _reloadEnded.Value = false;
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (!_reloadEnded.Value)
        {
            _currentTime -= deltaTime;

            if (_currentTime <= 0)
            {
                _reloaded?.Invoke();
                _reloadEnded.Value = true;
                _currentTime = _reloadTimer.Value;
            }
        }
    }


    public void Dispose(IEntity entity)
    {
        _shootEvent.Unsubscribe(OnShootEvent);
    }
}
