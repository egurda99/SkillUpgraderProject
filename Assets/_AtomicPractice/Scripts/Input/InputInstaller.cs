using Zenject;

public sealed class InputInstaller : MonoInstaller<InputInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<KeyboardInput>().AsSingle();
    }
}
