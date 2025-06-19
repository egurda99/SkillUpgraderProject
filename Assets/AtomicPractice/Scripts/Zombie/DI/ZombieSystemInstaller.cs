using Atomic.Entities;
using UnityEngine;
using Zenject;

public sealed class ZombieSystemInstaller : MonoInstaller<ZombieSystemInstaller>
{
    [SerializeField] private SceneEntity _zombiePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _zombieContainer;
    [SerializeField] private float _spawnInterval;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ZombieSpawner>()
            .AsSingle()
            .WithArguments(_zombiePrefab, _spawnPoints, _spawnInterval, _zombieContainer);


        Container.BindInterfacesAndSelfTo<ActiveZombiesProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<DeathZombiesCountProvider>().AsSingle();
    }
}
