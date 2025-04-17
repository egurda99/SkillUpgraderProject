using Zenject;

public sealed class MoneyStorageInstaller : MonoInstaller<MoneyStorageInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<MoneyStorage>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
