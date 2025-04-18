public interface ISaveLoader
{
    void LoadGame(IGameRepository repository, IContext context);

    void SaveGame(IGameRepository repository, IContext context);
}
