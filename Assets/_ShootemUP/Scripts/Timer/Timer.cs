using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class Timer : ITickable
    {
        private float _timeForTimer;

        private float _currentTime;
        public event Action OnTimerEnd;

        public void StartTimer(float time)
        {
            _timeForTimer = time;
            _currentTime = 0;
        }

        public void Tick()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _timeForTimer)
            {
                _currentTime = 0;
                OnTimerEnd?.Invoke();
            }
        }
    }
}
