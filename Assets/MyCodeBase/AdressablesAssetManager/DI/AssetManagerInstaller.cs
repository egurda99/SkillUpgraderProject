using Zenject;

namespace AssetManager.DI
{
    public sealed class AssetManagerInstaller : MonoInstaller<AssetManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AddressableAssetManager>().AsSingle().NonLazy();
        }
    }
}
