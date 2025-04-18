using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerPrefsGameStateSaver : IGameStateSaver
{
    private const string SAVE_KEY = "VALUE";

    public Dictionary<string, string> LoadData()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            var jsonData = PlayerPrefs.GetString(SAVE_KEY);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
        }

        return new Dictionary<string, string>();
    }

    public void SaveData(Dictionary<string, string> data)
    {
        var jsonData = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
    }
}
