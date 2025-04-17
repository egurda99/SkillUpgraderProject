using GameEngine;
using Zenject;

public sealed class UnitsSaveLoaderInstaller : MonoInstaller<UnitsSaveLoaderInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UnitsSaveLoader>().AsSingle().NonLazy();
    }
}