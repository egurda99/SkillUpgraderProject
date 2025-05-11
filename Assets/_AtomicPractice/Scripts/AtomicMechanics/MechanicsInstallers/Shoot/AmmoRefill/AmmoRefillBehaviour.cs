using Atomic.Elements;
using Atomic.Entities;

public sealed class AmmoRefillBehaviour : IEntityInit, IEntityUpdate
{
    private ReactiveVariable<float> _ammoRefillTime;
    private float _currentTime;
    private ReactiveVariable<bool> _isAmmoFull;
    private IEvent _refilled;
    private AndExpression _canRefill;

    public void Init(IEntity entity)
    {
        _ammoRefillTime = entity.GetAmmoRefillTime();

        _canRefill = entity.GetCanRefill();
        _currentTime = _ammoRefillTime.Value;
        _isAmmoFull = entity.GetIsAmmoFull();
        _refilled = entity.GetAmmoRefilled();
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_isAmmoFull.Value == false && _canRefill.Value)
        {
            _currentTime -= deltaTime;

            if (_currentTime <= 0)
            {
                _refilled?.Invoke();
                _currentTime = _ammoRefillTime.Value;
            }
        }
    }
}
