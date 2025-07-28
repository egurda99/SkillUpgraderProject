using AssetManager;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        private readonly AddressableAssetManager _assetManager;

        //TODO: Сделать через Addressables
        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }


        public MenuLoader(AddressableAssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public async UniTask LoadMenuAsync()
        {
            await _assetManager.LoadSceneAsync("Menu");
        }

        public async UniTask UnloadMenuAsync()
        {
            await _assetManager.UnloadSceneAsync("Menu");
        }
    }
}
