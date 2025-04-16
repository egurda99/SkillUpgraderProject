using Zenject;

public sealed class MoneySaveLoaderInstaller : MonoInstaller<MoneySaveLoaderInstaller>
{
    private ISaveLoader[] _saveLoaders;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MoneySaveLoader>().AsSingle().NonLazy();
    }
}