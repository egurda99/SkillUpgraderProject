using System;
using Zenject;

public sealed class KillsAdapter : IInitializable, IDisposable
{
    private readonly ValueView _killsView;
    private readonly DeathZombiesCountProvider _deathZombiesCountProvider;

    public KillsAdapter(ValueView killsView, DeathZombiesCountProvider deathZombiesCountProvider)
    {
        _killsView = killsView;
        _deathZombiesCountProvider = deathZombiesCountProvider;
    }

    public void Initialize()
    {
        _deathZombiesCountProvider.OnDeadZombiesCountChanged += OnKillsChanged;
        UpdateView(_deathZombiesCountProvider.DeadZombiesCount);
    }

    private void OnKillsChanged(int value)
    {
        UpdateView(value);
    }

    private void UpdateView(float value)
    {
        _killsView.SetupValue("KILLS: " + value);
    }


    public void Dispose()
    {
        _deathZombiesCountProvider.OnDeadZombiesCountChanged -= OnKillsChanged;
    }
}