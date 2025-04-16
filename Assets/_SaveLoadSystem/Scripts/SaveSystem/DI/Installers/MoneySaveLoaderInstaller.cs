using Zenject;

public sealed class MoneySaveLoaderInstaller : MonoInstaller<MoneySaveLoaderInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MoneySaveLoader>().AsSingle().NonLazy();
    }
}
