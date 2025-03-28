using Zenject;

namespace ShootEmUp
{
    public class GameFinisherInstaller : MonoInstaller<GameFinisherInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameFinisher>().AsSingle().NonLazy();
        }
    }
}
