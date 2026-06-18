using System.Collections.Generic;
using GameEngine;
using Newtonsoft.Json;

public sealed class GameRepository : IGameRepository
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

    public bool TryGetData<T>(string key, out T data)
    {
        if (_gameState.TryGetValue(key, out var jsonData))
        {
            data = JsonConvert.DeserializeObject<T>(jsonData, _jsonSettings);
            return data != null;
        }

        data = default;
        return false;
    }

    public void SetData<T>(string key, T data)
    {
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
