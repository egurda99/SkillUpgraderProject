using Cysharp.Threading.Tasks;
using static AssetManager.SceneKeys;

namespace AssetManager.Examples
{
    public sealed class MenuLoader
    {
        private readonly AddressableAssetManager _assetManager;

        public MenuLoader(AddressableAssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public async UniTask LoadMenuAsync()
        {
            await _assetManager.LoadSceneAsync(MENU);
        }

        public async UniTask UnloadMenuAsync()
        {
            await _assetManager.UnloadSceneAsync(MENU);
        }
    }
}
