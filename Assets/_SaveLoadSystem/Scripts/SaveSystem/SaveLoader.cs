using Zenject;

public abstract class SaveLoader<TService, TData> : ISaveLoader
{
    protected abstract TData ConvertToData(TService service);
    protected abstract void SetupData(TService service, TData data);

    protected virtual void SetupDefaultData(TService service)
    {
    }

    public void LoadGame(IGameRepository repository, DiContainer context)
    {
        var service = context.Resolve<TService>();

        if (repository.TryGetData(out TData data))
        {
            SetupData(service, data);
        }
        else
        {
            SetupDefaultData(service);
        }
    }

    public void SaveGame(IGameRepository repository, DiContainer context)
    {
        var service = context.Resolve<TService>();
        var data = ConvertToData(service);
        repository.SetData(data);
    }
}