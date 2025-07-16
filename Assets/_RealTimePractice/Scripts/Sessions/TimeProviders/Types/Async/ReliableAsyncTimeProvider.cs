using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RealTimePractice
{
    public sealed class ReliableAsyncTimeProvider : IAsyncTimeProvider
    {
        private readonly IAsyncTimeProvider _networkProvider;
        private readonly IAsyncTimeProvider _fallbackProvider;

        public ReliableAsyncTimeProvider(IAsyncTimeProvider networkProvider, IAsyncTimeProvider fallbackProvider)
        {
            _networkProvider = networkProvider;
            _fallbackProvider = fallbackProvider;
        }

        public async UniTask<DateTime> GetCurrentTimeAsync()
        {
            try
            {
                return await _networkProvider.GetCurrentTimeAsync();
            }
            catch (Exception e)
            {
                Debug.LogWarning(
                    $"[ReliableAsyncTimeProvider] Network time failed, falling back to local time. Error: {e.Message}");
                return await _fallbackProvider.GetCurrentTimeAsync();
            }
        }
    }
}
