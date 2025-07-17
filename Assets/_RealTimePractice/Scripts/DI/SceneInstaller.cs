using UnityEngine;
using Zenject;

namespace RealTimePractice
{
    public sealed class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private SceneInstallerHelper _helper;


        public override void InstallBindings()
        {
            BindSessions();

            BindChests();

            BindRewardManager();
        }

        private void BindRewardManager()
        {
            Container.BindInterfacesAndSelfTo<RewardManager>().AsSingle().WithArguments(_helper.RewardConfig).NonLazy();
        }

        private void BindChests()
        {
            Container.BindInterfacesAndSelfTo<ChestsSaveLoader>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChestsManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChestsTimeUpdater>().AsSingle().NonLazy();
        }

        private void BindSessions()
        {
            BindSessionManager();
            Container.BindInterfacesAndSelfTo<SessionSaveLoader>().AsSingle().NonLazy();
        }

        private void BindSessionManager()
        {
            if (_helper.UseAsync)
            {
                var networkAsyncTimeProvider = new NetworkAsyncTimeProvider();
                var fallbackAsyncTimeProvider = new FallbackAsyncTimeProvider();

                var asyncProvider = new ReliableAsyncTimeProvider(networkAsyncTimeProvider, fallbackAsyncTimeProvider);
                Container.Bind<IAsyncTimeProvider>().FromInstance(asyncProvider).AsSingle();
                Container.Bind<ITimeProvider>().FromInstance(asyncProvider).AsSingle();

                Container.Bind<ISessionManager>().To<AsyncSessionManager>().AsSingle();
            }
            else
            {
                var fallbackSyncTimeProvider = new FallbackSyncTimeProvider();


                var syncProvider = new ReliableSyncTimeProvider(fallbackSyncTimeProvider);

                Container.Bind<ISyncTimeProvider>().FromInstance(syncProvider).AsSingle();
                Container.Bind<ITimeProvider>().FromInstance(syncProvider).AsSingle();

                Container.Bind<ISessionManager>().To<SyncSessionManager>().AsSingle();
            }
        }
    }
}
