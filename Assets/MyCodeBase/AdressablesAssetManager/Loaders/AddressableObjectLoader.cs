using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace AssetManager
{
    public sealed class AddressableObjectLoader : IDisposable
    {
        private readonly Dictionary<string, AsyncOperationHandle<Object>> _loadedObjects = new();


        public async UniTask PreloadObjectAsync(string key)
        {
            if (_loadedObjects.ContainsKey(key))
                return;

            var handle = Addressables.LoadAssetAsync<Object>(key);
            _loadedObjects[key] = handle;
            await handle;

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception($"Failed to preload asset '{key}'");
        }

        public async UniTask<T> LoadObjectAsync<T>(string key, Action<float> onProgress = null) where T : Object
        {
            if (_loadedObjects.TryGetValue(key, out var existingHandle))
            {
                onProgress?.Invoke(1f);
                return existingHandle.Result as T ??
                       throw new Exception($"Can't cast loaded object '{key}' to {typeof(T).Name}");
            }

            var handle = Addressables.LoadAssetAsync<Object>(key);
            _loadedObjects[key] = handle;

            while (!handle.IsDone)
            {
                onProgress?.Invoke(handle.PercentComplete);
                await UniTask.Yield();
            }

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception($"Failed to load asset '{key}'");

            onProgress?.Invoke(1f);

            return handle.Result as T ?? throw new Exception($"Can't cast loaded object '{key}' to {typeof(T).Name}");
        }

        public async UniTask<T> LoadObjectAsync<T>(string key) where T : Object
        {
            return await LoadObjectAsync<T>(key, null);
        }

        public bool TryGetObject<T>(string key, out T result) where T : Object
        {
            if (_loadedObjects.TryGetValue(key, out var handle))
            {
                result = handle.Result as T ??
                         throw new Exception($"Can't cast loaded object '{key}' to {typeof(T).Name}");
                return true;
            }

            result = null;
            return false;
        }

        public T GetObject<T>(string key) where T : Object
        {
            if (!_loadedObjects.TryGetValue(key, out var handle))
                throw new Exception($"No loaded object with key '{key}'");

            return handle.Result as T ??
                   throw new Exception($"Can't cast loaded object '{key}' to {typeof(T).Name}");
        }

        public void Release(string key)
        {
            if (_loadedObjects.TryGetValue(key, out var handle))
            {
                Addressables.Release(handle);
                _loadedObjects.Remove(key);
            }
        }

        public void Dispose()
        {
            foreach (var handle in _loadedObjects.Values)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }

            _loadedObjects.Clear();
        }
    }
}
