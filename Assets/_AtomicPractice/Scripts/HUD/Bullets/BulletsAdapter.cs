using System;
using Atomic.Entities;
using Zenject;

public sealed class BulletsAdapter : IInitializable, IDisposable
{
    private readonly ValueView _bulletsView;
    private readonly SceneEntity _playerEntity;

    private int _currentAmmo;
    private int _maxAmmo;

    public BulletsAdapter(ValueView bulletsView, PlayerService playerService)
    {
        _bulletsView = bulletsView;
        _playerEntity = playerService.Player;
    }

    public void Initialize()
    {
        _playerEntity.GetCurrentAmmo().Subscribe(OnCurrentAmmoChanged);
        _playerEntity.GetMaxAmmo().Subscribe(OnMaxAmmoChanged);

        _currentAmmo = _playerEntity.GetCurrentAmmo().Value;
        _maxAmmo = _playerEntity.GetMaxAmmo().Value;
        UpdateView();
    }

    private void OnMaxAmmoChanged(int value)
    {
        _maxAmmo = value;
        UpdateView();
    }

    private void OnCurrentAmmoChanged(int value)
    {
        _currentAmmo = value;
        UpdateView();
    }


    private void UpdateView()
    {
        _bulletsView.SetupValue($"BULLETS: {_currentAmmo}/{_maxAmmo}");
    }


    public void Dispose()
    {
        _playerEntity.GetCurrentAmmo().Unsubscribe(OnCurrentAmmoChanged);
        _playerEntity.GetMaxAmmo().Unsubscribe(OnMaxAmmoChanged);
    }
}
