using System.Collections.Generic;
using _BehaviourTreePractice;
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
        [SerializeField] private Transform _botsContainer;
        [SerializeField] private Transform[] _spawnPoints;

        [Header("FOR AI")] [SerializeField] private List<Transform> _waypoints;
        [SerializeField] private Transform _conveyor;


        public override void InstallBindings()
        {
            BindConverter();
            BindMoneyStorage();
            BindTreesProvider();

            ConfigureBots();
        }

        private void BindTreesProvider()
        {
            Container.Bind<ActiveTreesProvider>().AsSingle();
        }

        private void ConfigureBots()
        {
            for (var index = 0; index < _spawnPoints.Length; index++)
            {
                var spawnPoint = _spawnPoints[index];
                var player = Container.InstantiatePrefabForComponent<SceneEntity>(_workerPrefab, spawnPoint.position,
                    Quaternion.identity, _botsContainer);

                var id = player.GetEntityID();

                var blackBoard = player.GetComponentInChildren<BehaviorTree>();
                var treeSensor = player.GetComponentInChildren<FindClosestTreeSensor>();


                var inventory = player.GetComponent<DebugInventory>();

                var backpackObserver = new BackpackObserver(inventory, blackBoard);
                Container.BindInterfacesTo<BackpackObserver>().FromInstance(backpackObserver).AsCached();


                var treeObserver = new TreeSensorObserver(treeSensor, blackBoard, id);

                Container.BindInterfacesTo<TreeSensorObserver>().FromInstance(treeObserver).AsCached();

                var sharedList = new SharedTransformList { Value = _waypoints };
                var sharedVector3 = new SharedVector3 { Value = _conveyor.position };

                blackBoard.SetVariable(WAYPOINTS, sharedList);
                blackBoard.SetVariable(CONVEYOR_POSITION, sharedVector3);
            }
        }

        private void BindConverter()
        {
            Container.BindInterfacesAndSelfTo<ConverterInstaller>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<ConveyorInputObserver>().AsSingle();
        }

        private void BindMoneyStorage()
        {
            Container.Bind<MoneyStorage>().AsSingle();
        }
    }
}
