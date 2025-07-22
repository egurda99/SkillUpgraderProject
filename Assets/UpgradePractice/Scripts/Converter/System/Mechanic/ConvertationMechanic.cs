using System;

namespace _UpgradePractice.Scripts
{
    public sealed class ConvertationMechanic : IDisposable
    {
        private readonly ConvertTimerController _timer;
        private readonly ConverterData _data;
        private readonly CompositeCondition _canConvert = new();

        private bool _isConverting;
        private bool _isReadyToConvert;

        public event Action OnStarted;
        public event Action OnCompleted;
        public event Action<float> OnProgress;

        public ConvertationMechanic(ConverterData data, ConvertTimerController timer)
        {
            _data = data;
            _timer = timer;

            _timer.OnElapsed += OnTimerEnded;
            _timer.OnProgressChanged += OnProgressChanged;
        }

        public void SetConditions(params Func<bool>[] conditions)
        {
            foreach (var cond in conditions)
                _canConvert.AppendCondition(cond);
        }

        public void Tick()
        {
            _timer.Tick();

            if (_isReadyToConvert)
            {
                _isReadyToConvert = false;
                _isConverting = false;
                OnCompleted?.Invoke();
            }

            if (!_isConverting && _canConvert.Invoke())
                StartConvertation();
        }

        private void StartConvertation()
        {
            _timer.UpdateConvertTime(_data.ÑonvertTime);
            _timer.Start();
            _isConverting = true;
            _isReadyToConvert = false;
            OnStarted?.Invoke();
        }

        private void OnTimerEnded()
        {
            _isReadyToConvert = true;
        }

        private void OnProgressChanged(float progress)
        {
            OnProgress?.Invoke(progress);
        }

        public void Dispose()
        {
            _timer.OnElapsed -= OnTimerEnded;
            _timer.OnProgressChanged -= OnProgressChanged;
        }
    }
}