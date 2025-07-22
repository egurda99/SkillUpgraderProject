using System.Collections.Generic;
using _UpgradePractice.Scripts;
using Atomic.Entities;
using BehaviorDesigner.Runtime;
using UnityEngine;
using Zenject;
using static _BehaviourTreePractice.BlackboardKeys;

namespace BehaviourTreePractice
{
    public sealed class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private SceneEntity _workerPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private List<Transform> _waypoints;
        [SerializeField] private Transform _botsContainer;


        public override void InstallBindings()
        {
            BindConverter();
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

                var blackBoard = player.GetComponentInChildren<BehaviorTree>();
                var sharedList = new SharedTransformList { Value = _waypoints };

                blackBoard.SetVariable(WAYPOINTS, sharedList);
            }
        }

        private void BindConverter()
        {
            Container.BindInterfacesAndSelfTo<ConverterInstaller>().FromComponentInHierarchy().AsSingle();
        }

        private void BindMoneyStorage()
        {
            Container.Bind<MoneyStorage>().AsSingle();
        }
    }
}
