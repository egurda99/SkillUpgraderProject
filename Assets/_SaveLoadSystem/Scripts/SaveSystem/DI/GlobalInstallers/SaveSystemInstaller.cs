using Zenject;

public sealed class SaveSystemInstaller : MonoInstaller<SaveSystemInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameRepository>().AsSingle();

        Container.Bind<SaveLoadManager>().AsSingle().NonLazy();
    }
}
