using Zenject;

namespace ShootEmUp
{
    public sealed class GameCycleInstaller : MonoInstaller<GameCycleInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameCycleManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
