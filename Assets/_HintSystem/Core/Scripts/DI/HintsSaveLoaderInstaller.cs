using Zenject;

namespace Game.Hints.DI
{
    public sealed class HintsSaveLoaderInstaller : MonoInstaller<HintsSaveLoaderInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HintsSaveLoader>().AsSingle().NonLazy();
        }
    }
}
