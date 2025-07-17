using System;

namespace RealTimePractice
{
    public sealed class FallbackSyncTimeProvider : ISyncTimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}