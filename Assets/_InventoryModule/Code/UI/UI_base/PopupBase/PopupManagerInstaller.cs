using Zenject;

namespace MyCodeBase
{
    public sealed class PopupManagerInstaller : MonoInstaller<PopupManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PopupManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
