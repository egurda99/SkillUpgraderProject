using _UpgradePractice.Scripts;
using Atomic.Entities;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public sealed class PlayerInstaller : MonoInstaller<PlayerInstaller>
{
    [SerializeField] private SceneEntity _playerPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _playerContainer;
    [SerializeField] private CinemachineCamera _playerCamera;


    public override void InstallBindings()
    {
        var player = Container.InstantiatePrefabForComponent<SceneEntity>(_playerPrefab, _spawnPoint.position,
            Quaternion.identity, _playerContainer);
        Container.Bind<PlayerService>().AsSingle().WithArguments(player);

        AddStartResoucesToPlayer(player);

        _playerCamera.Follow = player.transform;
    }

    private void AddStartResoucesToPlayer(SceneEntity player)
    {
        player.GetComponentInChildren<DebugInventory>().AddItem(new ResourceItem(ResourceType.Wood, 4));
    }
}
