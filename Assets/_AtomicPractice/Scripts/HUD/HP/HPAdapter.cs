using System;
using Atomic.Entities;
using Zenject;

public sealed class HPAdapter : IInitializable, IDisposable
{
    private readonly ValueView _hpView;
    private readonly SceneEntity _playerEntity;

    public HPAdapter(ValueView hpView, PlayerService playerService)
    {
        _hpView = hpView;
        _playerEntity = playerService.Player;
    }

    public void Initialize()
    {
        _playerEntity.GetHitPoints().Subscribe(OnHPChanged);
        UpdateView(_playerEntity.GetHitPoints().Value);
    }

    private void OnHPChanged(float value)
    {
        UpdateView(value);
    }


    private void UpdateView(float value)
    {
        _hpView.SetupValue("HIT POINTS: " + value);
    }


    public void Dispose()
    {
        _playerEntity.GetHitPoints().Unsubscribe(OnHPChanged);
    }
}
