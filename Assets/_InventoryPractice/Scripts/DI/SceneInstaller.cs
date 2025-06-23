using InventoryPractice;
using Zenject;

public class SceneInstaller : MonoInstaller<SceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<Inventory>().AsSingle();
    }
}
