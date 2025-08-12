using Zenject;

public sealed class ResourcesSaveLoaderInstaller : MonoInstaller<ResourcesSaveLoaderInstaller>
{
    public override void InstallBindings()
    {
        // Container.BindInterfacesAndSelfTo<ResourcesSaveLoader>().AsSingle().NonLazy();
    }
}
