using System;

namespace RealTimePractice
{
    public sealed class ReliableSyncTimeProvider : ISyncTimeProvider
    {
        private readonly ISyncTimeProvider _fallbackProvider;

        public ReliableSyncTimeProvider(ISyncTimeProvider fallbackProvider)
        {
            _fallbackProvider = fallbackProvider;
        }

        public DateTime GetCurrentTime()
        {
            return _fallbackProvider.GetCurrentTime();
        }
    }
}