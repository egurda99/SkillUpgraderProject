using System;
using Cysharp.Threading.Tasks;

namespace RealTimePractice
{
    public sealed class FallbackAsyncTimeProvider : IAsyncTimeProvider
    {
        public UniTask<DateTime> GetCurrentTimeAsync()
        {
            return UniTask.FromResult(DateTime.UtcNow);
        }
    }
}
