using Zenject;

namespace Game.Tutorial.DI
{
    public sealed class TutorialSaveLoaderInstaller : MonoInstaller<TutorialSaveLoaderInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TutorialSaveLoader>().AsSingle().NonLazy();
        }
    }
}
