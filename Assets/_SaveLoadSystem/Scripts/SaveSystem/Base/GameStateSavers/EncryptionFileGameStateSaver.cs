using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public sealed class EncryptionFileGameStateSaver : IGameStateSaver
{
    private const string FILE_NAME = "gamestate.sav";
    private readonly string _filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);
    private readonly EncryptionHelper _encryptionHelper = new();

    public Dictionary<string, string> LoadData()
    {
        if (!File.Exists(_filePath))
            return new Dictionary<string, string>();

        try
        {
            var encryptedBytes = File.ReadAllBytes(_filePath);
            var jsonData = _encryptionHelper.Decrypt(encryptedBytes);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
        }
        catch (Exception e)
        {
            Debug.LogError($"[EncryptionFileGameSaver] Failed to load: {e}");
            return new Dictionary<string, string>();
        }
    }

    public void SaveData(Dictionary<string, string> data)
    {
        try
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var encrypted = _encryptionHelper.Encrypt(jsonData);
            File.WriteAllBytes(_filePath, encrypted);
        }
        catch (Exception e)
        {
            Debug.LogError($"[EncryptionFileGameSaver] Failed to save: {e}");
        }
    }
}
