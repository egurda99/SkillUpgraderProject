using System;

namespace RealTimePractice
{
    [Serializable]
    public sealed class GameSession
    {
        private DateTime _startTime;
        private DateTime _endTime;

        public TimeSpan Duration => _endTime - _startTime;

        public DateTime StartTime => _startTime;

        public DateTime EndTime => _endTime;

        public GameSession(DateTime startTime)
        {
            _startTime = startTime;
        }

        public GameSession(DateTime dataSessionStartTime, DateTime dataSessionEndTime)
        {
            _startTime = dataSessionStartTime;
            _endTime = dataSessionEndTime;
        }

        public void End(DateTime endTime)
        {
            _endTime = endTime;
        }

        public override string ToString()
        {
            return $"Session: {_startTime:G} - {_endTime:G} (Duration: {Duration.TotalMinutes:F1} min)";
        }
    }
}
