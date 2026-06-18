public abstract class SaveLoader<TService, TData> : ISaveLoader
{
    protected virtual string Key => typeof(TData).Name;

    protected abstract TData ConvertToData(TService service);
    protected abstract void SetupData(TService service, TData data);

    protected virtual void SetupDefaultData(TService service)
    {
    }

    public void LoadGame(IGameRepository repository, IContext context)
    {
        var service = context.GetService<TService>();

        if (repository.TryGetData(Key, out TData data))
        {
            SetupData(service, data);
        }
        else
        {
            SetupDefaultData(service);
        }
    }

    public void SaveGame(IGameRepository repository, IContext context)
    {
        var service = context.GetService<TService>();
        var data = ConvertToData(service);
        repository.SetData(Key, data);
    }
}
