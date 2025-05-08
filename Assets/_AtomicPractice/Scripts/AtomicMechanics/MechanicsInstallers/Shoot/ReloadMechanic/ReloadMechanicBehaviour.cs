using Atomic.Elements;
using Atomic.Entities;

public sealed class ReloadMechanicBehaviour : IEntityInit, IEntityUpdate
{
    private ReactiveVariable<float> _reloadTime;
    private ReactiveVariable<bool> _reloadEnded;
    private float _currentTime;
    private ReactiveVariable<bool> _isAmmoFull;
    private IEvent _reloaded;

    public void Init(IEntity entity)
    {
        _reloadTime = entity.GetReloadTime();
        _reloadEnded = entity.GetReloadEnded();
        _reloadEnded.Value = true;
        _currentTime = _reloadTime.Value;
        _isAmmoFull = entity.GetIsAmmoFull();
        _reloaded = entity.GetReloaded();
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_isAmmoFull.Value == false)
        {
            _currentTime -= deltaTime;
            _reloadEnded.Value = false;
            if (_currentTime <= 0)
            {
                _reloaded?.Invoke();
                _currentTime = _reloadTime.Value;
            }
        }

        else
        {
            _reloadEnded.Value = true;
        }
    }
}
