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


            var playerMoveController = new MoveController(player.GetComponent<MoveComponent>());
            Container.QueueForInject(playerMoveController);

            Container.BindInterfacesTo<MoveController>().FromInstance(playerMoveController).AsSingle();

            var playerShootController = new ShootController(player.GetComponent<ShootComponent>());
            Container.QueueForInject(playerShootController);

            var playerDeathObserver = new CharacterDeathObserver(player.GetComponent<HealthComponent>());
            Container.QueueForInject(playerDeathObserver);
        }
    }
}
