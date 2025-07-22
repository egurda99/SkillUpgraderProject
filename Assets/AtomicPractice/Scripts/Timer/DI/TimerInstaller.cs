using MyTimer;
using Zenject;

public class TimerInstaller : MonoInstaller<TimerInstaller>
{
    public override void InstallBindings()
    {
        // Container.BindInterfacesAndSelfTo<Timer>().AsCached();
        Container.Bind<Timer>().AsTransient();
    }
}
