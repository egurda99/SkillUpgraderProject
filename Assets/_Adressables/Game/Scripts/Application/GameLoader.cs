using AssetManager;
using Cysharp.Threading.Tasks;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private readonly AddressableAssetManager _assetManager;

        // //TODO: Сделать через Addressables
        // public void UnloadGame()
        // {
        //     SceneManager.UnloadSceneAsync("Game");
        // }
        //
        // //TODO: Сделать через Addressables
        // public void LoadGame()
        // {
        //     SceneManager.LoadScene("Game");
        // }


        public GameLoader(AddressableAssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public async UniTask LoadGameAsync()
        {
            await _assetManager.LoadSceneAsync("Game");
        }

        public async UniTask UnloadGameAsync()
        {
            await _assetManager.UnloadSceneAsync("Game");
        }
    }
}
