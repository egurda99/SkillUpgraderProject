using Zenject;

namespace Game.Hints.DI
{
    public sealed class HintManagerInstaller : MonoInstaller<HintManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<HintManager>().AsSingle();
        }
    }
}
