using System;
using MyTimer;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class ConvertTimerController : IDisposable
    {
        private readonly Timer _timer;
        private float _convertTime;
        private readonly ConverterData _data;

        public event Action OnElapsed;
        public event Action<float> OnProgressChanged;

        public ConvertTimerController(Timer timer, ConverterData data)
        {
            _data = data;
            _timer = timer;
            _convertTime = _data.ÑonvertTime;
            _timer.SetInterval(_convertTime);
            _timer.OnElapsed += HandleElapsed;
            _timer.OnCurrentTimeChanged += HandleProgress;
        }

        public void Start()
        {
            _timer.SetInterval(_convertTime);
            _timer.Reset();
            _timer.Start();
        }

        public void Tick()
        {
            _timer.Tick();
        }

        public void UpdateConvertTime(float newTime)
        {
            _convertTime = newTime;
            _timer.SetInterval(_convertTime);
        }

        private void HandleElapsed()
        {
            OnElapsed?.Invoke();
        }

        private void HandleProgress(float currentTime)
        {
            var progress = currentTime / _convertTime;
            OnProgressChanged?.Invoke(Mathf.Clamp01(progress));
        }

        public void Dispose()
        {
            _timer.OnElapsed -= HandleElapsed;
            _timer.OnCurrentTimeChanged -= HandleProgress;
        }
    }
}
