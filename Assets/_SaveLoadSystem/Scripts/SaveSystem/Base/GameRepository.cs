using System.Collections.Generic;
using Newtonsoft.Json;

public class GameRepository : IGameRepository
{
    private Dictionary<string, string> _gameState = new();
    private readonly IGameStateSaver _gameStateSaver = new PlayerPrefsGameStateSaver();

    public bool TryGetData<T>(out T data)
    {
        var key = typeof(T).Name;

        if (_gameState.TryGetValue(key, out var jsonData))
        {
            data = JsonConvert.DeserializeObject<T>(jsonData);
            return true;
        }

        data = default;
        return false;
    }

    public void SetData<T>(T data)
    {
        var key = typeof(T).Name;

        var jsonData = JsonConvert.SerializeObject(data);
        _gameState[key] = jsonData;
    }

    public void SaveState()
    {
        _gameStateSaver.SaveData(_gameState);
    }

    public void LoadState()
    {
        _gameState = _gameStateSaver.LoadData();
    }
}
