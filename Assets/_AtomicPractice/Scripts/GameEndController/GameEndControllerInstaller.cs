using Zenject;

public sealed class GameEndControllerInstaller : MonoInstaller<GameEndControllerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameEndController>().AsSingle();
    }
}