using Atomic.Elements;
using Atomic.Entities;

public sealed class AmmoBehaviour : IEntityInit, IEntityDispose
{
    private ReactiveVariable<bool> _isAmmoFull;
    private IEvent _reloaded;
    private IEvent _shootEvent;

    private ReactiveVariable<int> _currentAmmo;
    private ReactiveVariable<int> _maxAmmo;
    private ReactiveVariable<int> _ammoAfterReload;
    private ReactiveVariable<bool> _needReload;
    private ReactiveVariable<bool> _isAmmoEmpty;

    public void Init(IEntity entity)
    {
        _currentAmmo = entity.GetCurrentAmmo();
        _maxAmmo = entity.GetMaxAmmo();
        _ammoAfterReload = entity.GetAmountAmmoAfterReload();
        _needReload = entity.GetNeedReload();

        _isAmmoEmpty = entity.GetIsAmmoEmpty();
        _isAmmoFull = entity.GetIsAmmoFull();
        _reloaded = entity.GetReloaded();
        _shootEvent = entity.GetShootEvent();

        _reloaded.Subscribe(OnReloaded);
        _shootEvent.Subscribe(OnShootEvent);


        _currentAmmo.Subscribe(OnCurrentAmmoChanged);
    }

    private void OnShootEvent()
    {
        _currentAmmo.Value -= 1;
    }

    private void OnCurrentAmmoChanged(int currentValue)
    {
        if (currentValue < _maxAmmo.Value && currentValue > 0)
        {
            _isAmmoFull.Value = false;
            _isAmmoEmpty.Value = false;
            _needReload.Value = true;
        }

        if (currentValue == 0)
        {
            _isAmmoFull.Value = false;
            _isAmmoEmpty.Value = true;
            _needReload.Value = true;
        }

        if (currentValue >= _maxAmmo.Value)
        {
            _needReload.Value = false;
            _isAmmoFull.Value = true;
        }
    }

    private void OnReloaded()
    {
        _currentAmmo.Value += _ammoAfterReload.Value;

        if (_currentAmmo.Value > _maxAmmo.Value)
        {
            _currentAmmo.Value = _maxAmmo.Value;
        }
    }

    public void Dispose(IEntity entity)
    {
        _reloaded.Unsubscribe(OnReloaded);
        _shootEvent.Unsubscribe(OnShootEvent);
    }
}
