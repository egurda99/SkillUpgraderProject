using System;
using UnityEngine;

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
                Debug.Log($"<color=red>Timer. {_currentTime} , {deltaTime}</color>");
                OnTimerEnd?.Invoke();
            }
        }
    }
}
