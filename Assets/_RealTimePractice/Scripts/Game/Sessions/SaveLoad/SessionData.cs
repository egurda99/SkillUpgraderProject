using System;

namespace RealTimePractice
{
    [Serializable]
    public sealed class SessionData
    {
        public DateTime StartTime;
        public DateTime EndTime;

        public TimeSpan Duration => EndTime - StartTime;

        public SessionData(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}