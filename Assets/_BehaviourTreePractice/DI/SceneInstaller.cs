using _UpgradePractice.Scripts;
using Atomic.Entities;
using UnityEngine;
using Zenject;

namespace BehaviourTreePractice
{
    public sealed class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private SceneEntity _workerPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _botsContainer;


        public override void InstallBindings()
        {
            BindContainer();
            BindMoneyStorage();
            BindTreesProvider();

            SpawnBots();
        }

        private void BindTreesProvider()
        {
            Container.Bind<ActiveTreesProvider>().AsSingle();
        }

        private void SpawnBots()
        {
            for (var index = 0; index < _spawnPoints.Length; index++)
            {
                var spawnPoint = _spawnPoints[index];
                var player = Container.InstantiatePrefabForComponent<SceneEntity>(_workerPrefab, spawnPoint.position,
                    Quaternion.identity, _botsContainer);
            }
        }

        private void BindContainer()
        {
            Container.BindInterfacesAndSelfTo<ConverterInstaller>().FromComponentInHierarchy().AsSingle();
        }

        private void BindMoneyStorage()
        {
            Container.Bind<MoneyStorage>().AsSingle();
        }
    }
}
