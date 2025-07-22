using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MyTimer
{
    [Serializable]
    public sealed class Timer : IInitializable, ITickable
    {
        private float _interval;
        [ShowInInspector] [ReadOnly] private float _time;
        private bool _isRunning;

        public event Action OnElapsed;
        public event Action<float> OnCurrentTimeChanged;


        void IInitializable.Initialize()
        {
            _time = 0;
        }

        public void Start() => _isRunning = true;
        public void Stop() => _isRunning = false;
        public void Reset() => _time = 0f;

        public void SetInterval(float interval)
        {
            _interval = interval;
        }

        public void SetInterval(int interval)
        {
            _interval = interval;
        }


        public void Tick()
        {
            if (!_isRunning)
                return;
            _time += Time.deltaTime;
            OnCurrentTimeChanged?.Invoke(_time);
            if (_time >= _interval)
            {
                _time = 0f;
                OnCurrentTimeChanged?.Invoke(_time);
                OnElapsed?.Invoke();
            }
        }
    }
}
