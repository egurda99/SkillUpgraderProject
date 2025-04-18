using System.Collections.Generic;
using GameEngine;
using Newtonsoft.Json;

public class GameRepository : IGameRepository
{
    private Dictionary<string, string> _gameState = new();

    private readonly IGameStateSaver _gameStateSaver = new EncryptionFileGameStateSaver();

    private static readonly JsonSerializerSettings _jsonSettings = new()
    {
        Converters = new List<JsonConverter>
        {
            new Vector3Converter()
        },
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    public bool TryGetData<T>(out T data)
    {
        var key = typeof(T).Name;

        if (_gameState.TryGetValue(key, out var jsonData))
        {
            data = JsonConvert.DeserializeObject<T>(jsonData, _jsonSettings);
            return data != null;
        }

        data = default;
        return false;
    }

    public void SetData<T>(T data)
    {
        var key = typeof(T).Name;
        var jsonData = JsonConvert.SerializeObject(data, _jsonSettings);
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
