using Atomic.Elements;
using Atomic.Entities;

public sealed class AutoShootBehaviour : IEntityInit, IEntityUpdate
{
    private AndExpression _canShoot;
    private IEvent _shootAction;

    public void Init(IEntity entity)
    {
        _canShoot = entity.GetCanShoot();
        _shootAction = entity.GetShootAction();
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canShoot.Value)
        {
            _shootAction?.Invoke();
        }
    }
}
