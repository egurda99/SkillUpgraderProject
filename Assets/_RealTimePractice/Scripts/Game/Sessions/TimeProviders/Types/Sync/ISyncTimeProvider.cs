using System;

namespace RealTimePractice
{
    public interface ISyncTimeProvider : ITimeProvider
    {
        DateTime GetCurrentTime();
    }
}