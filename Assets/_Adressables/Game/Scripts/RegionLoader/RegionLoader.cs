using AssetManager;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Adressables
{
    public sealed class RegionLoader
    {
        private readonly AddressableAssetManager _assetManager;

        public RegionLoader(AddressableAssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public void LoadRegion(string regionKey)
        {
            _assetManager.LoadSceneAsync(regionKey, LoadSceneMode.Additive).Forget();
        }

        public void UnLoadRegion(string regionKey)
        {
            _assetManager.UnloadSceneAsync(regionKey).Forget();
        }
    }
}