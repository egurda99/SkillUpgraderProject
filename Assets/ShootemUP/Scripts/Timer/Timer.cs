using System;

namespace ShootEmUp
{
    public sealed class Timer
    {
        private float _timeForTimer;

        private float _currentTime;
        public event Action OnTimerEnd;

        public void StartTimer(float time)
        {
            _timeForTimer = time;
            _currentTime = 0;
        }

        public void UpdateTimer(float deltaTime)
        {
            _currentTime += deltaTime;
            if (_currentTime >= _timeForTimer)
            {
                _currentTime = 0;
                OnTimerEnd?.Invoke();
            }
        }
    }
}
