using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class DestroyByLifeTimeBehaviour : IEntityInit, IEntityDisable, IEntityUpdate
{
    private ReactiveVariable<float> _lifeTime;
    private Transform _rootTransform;
    private AndExpression _canStartTimer;
    private Timer _timer;

    public void Init(IEntity entity)
    {
        _lifeTime = entity.GetLifeTime();
        _rootTransform = entity.GetRootTransform();
        _canStartTimer = entity.GetCanStartTimer();

        _timer = entity.GetLifetimeTimer();
        _timer.SetDuration(_lifeTime.Value);
        _timer.Start();
        _timer.OnEnded += OnTimerEnded;
    }

    private void OnTimerEnded()
    {
        Object.Destroy(_rootTransform.gameObject);
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canStartTimer.Value)
        {
            _timer.Tick(deltaTime);
        }
    }

    public void Disable(IEntity entity)
    {
        _timer.OnEnded -= OnTimerEnded;
    }
}
