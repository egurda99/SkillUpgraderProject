using System;
using Zenject;

public class DeathZombiesCountProvider : IInitializable, IDisposable
{
    private readonly ActiveZombiesProvider _activeZombiesProvider;
    private int _deadZombiesCount;

    public int DeadZombiesCount => _deadZombiesCount;

    public event Action<int> OnDeadZombiesCountChanged;


    public DeathZombiesCountProvider(ActiveZombiesProvider activeZombiesProvider)
    {
        _activeZombiesProvider = activeZombiesProvider;
        _deadZombiesCount = 0;
    }

    private void OnZombieRemovedFromActive()
    {
        _deadZombiesCount++;
        OnDeadZombiesCountChanged?.Invoke(_deadZombiesCount);
    }

    public void Initialize()
    {
        _activeZombiesProvider.OnZombieRemoved += OnZombieRemovedFromActive;
    }

    public void Dispose()
    {
        _activeZombiesProvider.OnZombieRemoved -= OnZombieRemovedFromActive;
    }
}
