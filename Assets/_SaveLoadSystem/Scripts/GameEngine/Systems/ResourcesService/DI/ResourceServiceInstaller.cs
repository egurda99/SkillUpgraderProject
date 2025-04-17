using Zenject;

namespace GameEngine
{
    public sealed class ResourceServiceInstaller : MonoInstaller<ResourceServiceInstaller>
    {
        public override void InstallBindings()
        {
            var helper = FindAnyObjectByType<ResourceInstallerHelper>();

            Container.BindInterfacesAndSelfTo<ResourceService>()
                .AsSingle()
                .WithArguments(helper.ResourceContainer)
                .NonLazy();
        }
    }
}
