using Zenject;

public interface ISaveLoader
{
    void LoadGame(IGameRepository repository, DiContainer context);

    void SaveGame(IGameRepository repository, DiContainer context);
}
