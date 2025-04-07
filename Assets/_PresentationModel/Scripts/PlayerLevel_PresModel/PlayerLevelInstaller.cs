using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelInstaller : MonoInstaller<PlayerLevelInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerLevel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPresentationModel>().AsSingle();
        }
    }
}
