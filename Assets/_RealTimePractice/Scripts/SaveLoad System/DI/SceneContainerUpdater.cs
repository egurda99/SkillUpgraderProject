using UnityEngine;
using Zenject;

public sealed class SceneContainerUpdater : MonoBehaviour
{
    private DiContainer _container;
    private SaveLoadManager _saveLoadManager;


    [Inject]
    public void Construct(SaveLoadManager saveLoadManager, DiContainer container)
    {
        _saveLoadManager = saveLoadManager;
        _container = container;
    }

    private void Start()
    {
        _saveLoadManager.InitOnNewScene(_container);
    }
}