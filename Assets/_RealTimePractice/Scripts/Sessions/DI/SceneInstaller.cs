using UnityEngine;
using Zenject;

namespace RealTimePractice
{
    public sealed class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private bool _useAsync = true;

        public override void InstallBindings()
        {
            if (_useAsync)
            {
                var networkAsyncTimeProvider = new NetworkAsyncTimeProvider();
                var fallbackAsyncTimeProvider = new FallbackAsyncTimeProvider();


                Container.Bind<IAsyncTimeProvider>().To<ReliableAsyncTimeProvider>()
                    .AsSingle()
                    .WithArguments(networkAsyncTimeProvider, fallbackAsyncTimeProvider);

                Container.Bind<ISessionManager>().To<AsyncSessionManager>().AsSingle();
            }
            else
            {
                var fallbackSyncTimeProvider = new FallbackSyncTimeProvider();


                Container.Bind<ISyncTimeProvider>().To<ReliableSyncTimeProvider>()
                    .AsSingle()
                    .WithArguments(fallbackSyncTimeProvider);

                Container.Bind<ISessionManager>().To<SyncSessionManager>().AsSingle();
            }

            Container.BindInterfacesAndSelfTo<SessionSaveLoader>().AsSingle().NonLazy();
        }
    }
}
