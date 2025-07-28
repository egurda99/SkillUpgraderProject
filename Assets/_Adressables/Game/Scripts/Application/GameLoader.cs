using AssetManager;
using Cysharp.Threading.Tasks;
using static AssetManager.SceneKeys;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private readonly AddressableAssetManager _assetManager;

        public GameLoader(AddressableAssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public async UniTask LoadGameAsync()
        {
            await _assetManager.LoadSceneAsync(GAME);
        }

        public async UniTask UnloadGameAsync()
        {
            await _assetManager.UnloadSceneAsync(GAME);
        }
    }
}
