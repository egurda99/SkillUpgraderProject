using System;
using Sirenix.OdinInspector;
using Zenject;

public sealed class SaveLoadManager
{
    private ISaveLoader[] _saveLoaders;

    private readonly GameRepository _repository;

    private readonly IContext _gameContext;

    public event Action OnStartSaving;
    public event Action OnLoaded;

    public SaveLoadManager(GameRepository repository, DiContainer diContainer)
    {
        _repository = repository;
        _gameContext = new ZenjectContext(diContainer);
    }

    public void InitOnNewScene(DiContainer container)
    {
        _gameContext.UpdateContainer(container);

        _saveLoaders = _gameContext.GetServices<ISaveLoader>();
    }

    [Button]
    public void Load()
    {
        _repository.LoadState();

        foreach (var saveLoader in _saveLoaders)
        {
            saveLoader.LoadGame(_repository, _gameContext);
        }

        OnLoaded?.Invoke();
    }

    [Button]
    public void Save()
    {
        OnStartSaving?.Invoke();
        foreach (var saveLoader in _saveLoaders)
        {
            saveLoader.SaveGame(_repository, _gameContext);
        }

        _repository.SaveState();
    }
}
