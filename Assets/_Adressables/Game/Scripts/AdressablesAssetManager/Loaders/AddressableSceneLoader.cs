using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AssetManager
{
    public sealed class AddressableSceneLoader : IDisposable
    {
        private readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> _loadedScenes = new();

        public async UniTask LoadSceneAsync(string key, LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true, int priority = 100)
        {
            if (_loadedScenes.ContainsKey(key))
                return;

            var handle = Addressables.LoadSceneAsync(key, mode, activateOnLoad, priority);
            _loadedScenes[key] = handle;
            await handle;

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception($"Failed to load scene '{key}'");
        }

        public async UniTask UnloadSceneAsync(string key)
        {
            if (_loadedScenes.TryGetValue(key, out var handle))
            {
                var unloadHandle = Addressables.UnloadSceneAsync(handle);
                await unloadHandle;
                _loadedScenes.Remove(key);
            }
        }

        public void Dispose()
        {
            foreach (var handle in _loadedScenes.Values)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }

            _loadedScenes.Clear();
        }
    }
}
