using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField] private Transform _enemyContainerTransform;
        [SerializeField] private int _enemyPoolInitialSize;
        [SerializeField] private Enemy _enemyPrefab;


        public override void InstallBindings()
        {
            Container.BindMemoryPool<Enemy, Enemy.Pool>().WithInitialSize(_enemyPoolInitialSize)
                .FromComponentInNewPrefab(_enemyPrefab).UnderTransform(_enemyContainerTransform).AsCached();

            Container.BindInterfacesAndSelfTo<EnemyPositionsHandler>().FromComponentsInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyConfigurer>().AsSingle();
            Container.BindInterfacesAndSelfTo<ActiveEnemiesProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesGameCycleUpdater>().AsSingle();
        }
    }
}
