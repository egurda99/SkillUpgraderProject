using _Adressables;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraConfig cameraConfig;

        [SerializeField] private new Camera camera;

        [SerializeField] private InputConfig inputConfig;

        public override void InstallBindings()
        {
            Container
                .Bind<Camera>()
                .FromInstance(camera);

            Container
                .Bind<ICharacter>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .BindInterfacesTo<MoveController>()
                .AsCached()
                .NonLazy();

            Container
                .Bind<IMoveInput>()
                .To<MoveInput>()
                .AsSingle()
                .WithArguments(inputConfig)
                .NonLazy();

            Container
                .BindInterfacesTo<CameraFollower>()
                .AsCached()
                .WithArguments(cameraConfig.cameraOffset)
                .NonLazy();

            Container.Bind<RegionLoader>().AsSingle();
        }
    }
}
