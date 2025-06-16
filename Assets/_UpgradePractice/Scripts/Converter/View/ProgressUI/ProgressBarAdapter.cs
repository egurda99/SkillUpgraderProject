using System;

namespace _UpgradePractice.Scripts
{
    public class ProgressBarAdapter : IDisposable
    {
        private readonly ProgressBar _progressBar;
        private readonly ConverterSystem _converterSystem;

        public ProgressBarAdapter(ProgressBar progressBar, ConverterSystem converterSystem)
        {
            _progressBar = progressBar;
            _converterSystem = converterSystem;

            _converterSystem.OnConvertProgressChanged += OnProgressChanged;
            _converterSystem.OnConvertCompleted += OnComplete;
            _converterSystem.OnConvertStarted += OnStarted;

            _progressBar.SetVisible(false);
        }

        private void OnStarted()
        {
            _progressBar.SetVisible(true);
        }

        private void OnComplete()
        {
            _progressBar.SetVisible(false);
        }

        private void OnProgressChanged(float value)
        {
            _progressBar.SetProgress(value);
        }

        public void Dispose()
        {
            _converterSystem.OnConvertProgressChanged -= OnProgressChanged;
            _converterSystem.OnConvertCompleted -= OnComplete;
            _converterSystem.OnConvertStarted -= OnStarted;
        }
    }
}
