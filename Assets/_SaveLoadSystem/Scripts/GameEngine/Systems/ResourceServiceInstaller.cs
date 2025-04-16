using Zenject;

namespace GameEngine
{
    public sealed class ResourceServiceInstaller : MonoInstaller<ResourceServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ResourceService>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
