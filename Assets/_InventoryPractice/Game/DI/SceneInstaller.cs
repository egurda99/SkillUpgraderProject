using _InventoryPractice;
using Zenject;

namespace InventoryPractice
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Inventory>().AsSingle();
            Container.Bind<Equipment>().AsSingle();
            Container.Bind<PlayerStats>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DetailsItemPresenterFactory>().AsSingle();
            Container.Bind<InventoryPopupViewModelFactory>().AsSingle();
        }
    }
}
