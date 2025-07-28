using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace AssetManager
{
    public sealed class AddressableAssetManager : IDisposable
    {
        private readonly AddressableObjectLoader _objectLoader = new();
        private readonly AddressablePrefabLoader _prefabLoader = new();
        private readonly AddressableSceneLoader _sceneLoader = new();

        public UniTask PreloadObjectAsync(string key)
        {
            return _objectLoader.PreloadObjectAsync(key);
        }

        public UniTask<T> LoadObjectAsync<T>(string key, Action<float> onProgress = null) where T : Object
        {
            return _objectLoader.LoadObjectAsync<T>(key, onProgress);
        }

        public bool TryGetObject<T>(string key, out T result) where T : Object
        {
            return _objectLoader.TryGetObject(key, out result);
        }

        public T GetObject<T>(string key) where T : Object
        {
            return _objectLoader.GetObject<T>(key);
        }

        public void ReleaseObject(string key)
        {
            _objectLoader.Release(key);
        }


        public UniTask PreloadPrefabAsync(string key)
        {
            return _prefabLoader.PreloadPrefabAsync(key);
        }

        public UniTask<GameObject> LoadPrefabAsync(string key, Action<float> onProgress = null)
        {
            return _prefabLoader.LoadPrefabAsync(key, onProgress);
        }

        public bool TryGetPrefab(string key, out GameObject result)
        {
            return _prefabLoader.TryGetPrefab(key, out result);
        }

        public GameObject GetPrefab(string key)
        {
            return _prefabLoader.GetPrefab(key);
        }

        public T GetPrefab<T>(string key) where T : Component
        {
            return _prefabLoader.GetPrefab<T>(key);
        }

        public void ReleasePrefab(string key)
        {
            _prefabLoader.Release(key);
        }


        public UniTask LoadSceneAsync(string key, LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true, int priority = 100)
        {
            return _sceneLoader.LoadSceneAsync(key, mode, activateOnLoad, priority);
        }

        public UniTask UnloadSceneAsync(string key)
        {
            return _sceneLoader.UnloadSceneAsync(key);
        }

        public void Dispose()
        {
            _objectLoader.Dispose();
            _prefabLoader.Dispose();
            _sceneLoader.Dispose();
        }
    }
}
