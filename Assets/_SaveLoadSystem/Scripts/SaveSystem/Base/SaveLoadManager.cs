using System;
using Sirenix.OdinInspector;
using Zenject;

public sealed class SaveLoadManager
{
    public event Action OnStartSaving;
    public event Action OnLoaded;

    private ISaveLoader[] _saveLoaders;
    private readonly GameRepository _repository;
    private DiContainer _diContainer;

    public SaveLoadManager(GameRepository repository, DiContainer diContainer)
    {
        _repository = repository;
        _diContainer = diContainer;
    }

    public void InitOnNewScene(DiContainer container)
    {
        _diContainer = container;

        _saveLoaders = _diContainer.ResolveAll<ISaveLoader>().ToArray();
    }

    [Button]
    public void Load()
    {
        _repository.LoadState();

        foreach (var saveLoader in _saveLoaders)
        {
            saveLoader.LoadGame(_repository, _diContainer);
        }

        OnLoaded?.Invoke();
    }

    [Button]
    public void Save()
    {
        OnStartSaving?.Invoke();
        foreach (var saveLoader in _saveLoaders)
        {
            saveLoader.SaveGame(_repository, _diContainer);
        }

        _repository.SaveState();
    }
}
