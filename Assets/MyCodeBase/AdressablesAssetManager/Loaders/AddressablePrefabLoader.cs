using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AssetManager
{
    public sealed class AddressablePrefabLoader : IDisposable
    {
        private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _loadedPrefabs = new();

        public async UniTask PreloadPrefabAsync(string key)
        {
            if (_loadedPrefabs.ContainsKey(key)) return;

            var handle = Addressables.LoadAssetAsync<GameObject>(key);
            _loadedPrefabs[key] = handle;
            await handle;
        }

        public async UniTask<GameObject> LoadPrefabAsync(string key, Action<float> onProgress = null)
        {
            if (_loadedPrefabs.TryGetValue(key, out var existingHandle))
            {
                onProgress?.Invoke(1f);
                return existingHandle.Result;
            }

            var handle = Addressables.LoadAssetAsync<GameObject>(key);
            _loadedPrefabs[key] = handle;

            while (!handle.IsDone)
            {
                onProgress?.Invoke(handle.PercentComplete);
                await UniTask.Yield();
            }

            onProgress?.Invoke(1f);

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception($"Failed to load prefab '{key}'");

            return handle.Result;
        }

        public bool TryGetPrefab(string key, out GameObject result)
        {
            if (_loadedPrefabs.TryGetValue(key, out var handle))
            {
                result = handle.Result;
                return true;
            }

            result = null;
            return false;
        }

        public GameObject GetPrefab(string key)
        {
            if (!_loadedPrefabs.TryGetValue(key, out var handle))
                throw new Exception($"No loaded prefab with key '{key}'");

            return handle.Result;
        }

        public T GetPrefab<T>(string key) where T : Component
        {
            var go = GetPrefab(key);
            var comp = go.GetComponent<T>();
            if (comp == null)
                throw new Exception($"Prefab '{key}' doesn't contain component of type {typeof(T).Name}");
            return comp;
        }

        public void Release(string key)
        {
            if (_loadedPrefabs.TryGetValue(key, out var handle))
            {
                Addressables.Release(handle);
                _loadedPrefabs.Remove(key);
            }
        }

        public void Dispose()
        {
            foreach (var handle in _loadedPrefabs.Values)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }

            _loadedPrefabs.Clear();
        }
    }
}
