using Atomic.Elements;
using Atomic.Entities;

public sealed class ShootReloadBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private IEvent _reloaded;
    private IEvent _shootEvent;
    private ReactiveVariable<float> _reloadTime;
    private ReactiveVariable<bool> _reloadEnded;
    private ReactiveVariable<bool> _needReload;
    private Timer _timer;

    public void Init(IEntity entity)
    {
        _reloadTime = entity.GetReloadTime();
        _reloaded = entity.GetReloaded();
        _reloadEnded = entity.GetReloadEnded();

        _needReload = entity.GetNeedReload();
        _shootEvent = entity.GetShootEvent();

        _timer = entity.GetReloadTimer();

        _timer.SetDuration(_reloadTime.Value);
        _timer.Start();

        _timer.OnEnded += OnReloadTimerEnded;
        _timer.OnStarted += OnReloadTimerStarted;
        _shootEvent.Subscribe(OnShootEvent);
    }

    private void OnReloadTimerStarted()
    {
        _reloadEnded.Value = false;
    }

    private void OnReloadTimerEnded()
    {
        _reloaded?.Invoke();
        _reloadEnded.Value = true;
        _needReload.Value = false;
        _timer.SetDuration(_reloadTime.Value);
    }

    private void OnShootEvent()
    {
        _needReload.Value = true;
        _timer.Start();
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_needReload.Value)
        {
            _timer.Tick(deltaTime);
        }
    }


    public void Dispose(IEntity entity)
    {
        _shootEvent.Unsubscribe(OnShootEvent);
        _timer.OnEnded -= OnReloadTimerEnded;
    }
}
