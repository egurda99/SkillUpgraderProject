using Atomic.Elements;
using Atomic.Entities;

public sealed class StunBehaviour : IEntityInit, IEntityDispose, IEntityUpdate
{
    private ReactiveVariable<float> _stunTime;

    private Timer _timer;
    private IEvent<float> _takeDamageEvent;
    private ReactiveVariable<bool> _isStunned;
    private ReactiveVariable<bool> _isDead;

    public void Init(IEntity entity)
    {
        _stunTime = entity.GetStunTime();
        _isStunned = entity.GetIsStunned();
        _isDead = entity.GetIsDead();

        _timer = entity.GetStunTimer();
        _timer.SetDuration(_stunTime.Value);
        _takeDamageEvent = entity.GetTakeDamageAction();

        _takeDamageEvent.Subscribe(OnGetDamage);
        _timer.OnEnded += OnStunTimerEnded;
    }

    private void OnGetDamage(float obj)
    {
        if (_isDead.Value)
            return;

        _isStunned.Value = true;
        _timer.SetDuration(_stunTime.Value);
        _timer.ForceStart();
    }

    private void OnStunTimerEnded()
    {
        _timer.SetDuration(_stunTime.Value);
        _isStunned.Value = false;
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        _timer.Tick(deltaTime);
    }

    public void Dispose(IEntity entity)
    {
        _timer.OnEnded -= OnStunTimerEnded;
        _takeDamageEvent.Unsubscribe(OnGetDamage);
    }
}
