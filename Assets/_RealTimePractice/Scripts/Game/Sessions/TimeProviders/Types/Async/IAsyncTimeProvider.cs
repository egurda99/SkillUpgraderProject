using System;
using Cysharp.Threading.Tasks;

namespace RealTimePractice
{
    public interface IAsyncTimeProvider : ITimeProvider
    {
        UniTask<DateTime> GetCurrentTimeAsync();
    }
}
