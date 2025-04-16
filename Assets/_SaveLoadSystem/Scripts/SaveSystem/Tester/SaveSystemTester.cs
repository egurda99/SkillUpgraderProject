using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public sealed class SaveSystemTester : MonoBehaviour
{
    private SaveLoadManager _saveLoadManager;

    [Inject]
    public void Construct(SaveLoadManager saveLoadManager)
    {
        _saveLoadManager = saveLoadManager;
    }

    [Button]
    public void Load()
    {
        _saveLoadManager.Load();
    }

    [Button]
    public void Save()
    {
        _saveLoadManager.Save();
    }
}
