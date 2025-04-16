using System.Collections.Generic;

public interface IGameStateSaver
{
    Dictionary<string, string> LoadData();
    void SaveData(Dictionary<string, string> data);
}