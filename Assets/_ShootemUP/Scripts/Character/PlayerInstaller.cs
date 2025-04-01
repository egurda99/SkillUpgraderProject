using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _worldContainer;


        public override void InstallBindings()
        {
            var player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _spawnPoint.position,
                Quaternion.identity, _worldContainer);

            Container.BindInterfacesAndSelfTo<Player>().FromInstance(player).AsSingle();


            Container.BindInterfacesAndSelfTo<MoveController>()
                .AsSingle()
                .WithArguments(player.GetComponent<MoveComponent>());

            Container.BindInterfacesAndSelfTo<ShootController>()
                .AsSingle()
                .WithArguments(player.GetComponent<ShootComponent>());

            Container.BindInterfacesAndSelfTo<CharacterDeathObserver>()
                .AsSingle()
                .WithArguments(player.GetComponent<HealthComponent>());
        }
    }
}
