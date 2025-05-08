using Atomic.Elements;
using Atomic.Entities;

public sealed class AutoShootAfterReloadShootBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private bool _reloadEnded;
    private IEvent _shootEvent;

    private AndExpression _canShoot;
    private IEvent _shootAction;
    private IEvent _reloaded;

    public void Init(IEntity entity)
    {
        _reloadEnded = false;

        _canShoot = entity.GetCanShoot();
        _shootEvent = entity.GetShootEvent();
        _shootAction = entity.GetShootAction();

        _reloaded = entity.GetReloaded();

        _shootEvent.Subscribe(OnShoot);
        _reloaded.Subscribe(OnReloaded);
    }

    private void OnReloaded()
    {
        _reloadEnded = true;
    }

    private void OnShoot()
    {
        _reloadEnded = false;
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canShoot.Value && _reloadEnded)
        {
            _shootAction?.Invoke();
        }
    }

    public void Dispose(IEntity entity)
    {
        _shootEvent.Unsubscribe(OnShoot);
        _reloaded.Unsubscribe(OnReloaded);
    }
}
