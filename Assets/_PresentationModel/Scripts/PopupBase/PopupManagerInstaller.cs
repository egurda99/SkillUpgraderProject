using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PopupManagerInstaller : MonoInstaller<PopupManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PopupViewModelFactory>().AsSingle();
            Container.Bind<PopupManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
