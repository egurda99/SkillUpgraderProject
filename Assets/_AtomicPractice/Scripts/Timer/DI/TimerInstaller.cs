using Zenject;

public class TimerInstaller : MonoInstaller<TimerInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Timer>().AsCached();
    }
}
