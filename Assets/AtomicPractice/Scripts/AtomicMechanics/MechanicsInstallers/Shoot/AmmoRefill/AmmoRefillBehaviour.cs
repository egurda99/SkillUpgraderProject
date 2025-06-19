using Atomic.Elements;
using Atomic.Entities;

public sealed class AmmoRefillBehaviour : IEntityInit, IEntityDispose, IEntityUpdate
{
    private ReactiveVariable<float> _ammoRefillTime;
    private ReactiveVariable<bool> _isAmmoFull;
    private IEvent _refilled;
    private AndExpression _canRefill;
    private Timer _timer;

    public void Init(IEntity entity)
    {
        _ammoRefillTime = entity.GetAmmoRefillTime();

        _canRefill = entity.GetCanRefill();
        _isAmmoFull = entity.GetIsAmmoFull();
        _refilled = entity.GetAmmoRefilled();

        _timer = entity.GetAmmoRefillTimer();
        _timer.Loop = true;
        _timer.SetDuration(_ammoRefillTime.Value);
        _timer.Start();

        _timer.OnEnded += OnRefillTimerEnded;
    }

    private void OnRefillTimerEnded()
    {
        _refilled?.Invoke();
        _timer.SetDuration(_ammoRefillTime.Value);
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_isAmmoFull.Value == false && _canRefill.Value)
        {
            _timer.Tick(deltaTime);
        }
    }

    public void Dispose(IEntity entity)
    {
        _timer.OnEnded -= OnRefillTimerEnded;
    }
}
