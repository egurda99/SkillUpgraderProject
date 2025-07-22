using System;

namespace _UpgradePractice.Scripts
{
    public sealed class WorkerAnimationAdapter : IDisposable
    {
        private readonly WorkerAnimationHandler _handler;
        private readonly ConverterSystem _converterSystem;

        public WorkerAnimationAdapter(WorkerAnimationHandler handler, ConverterSystem converterSystem)
        {
            _handler = handler;
            _converterSystem = converterSystem;

            _converterSystem.OnConvertStarted += OnWorkStarted;
            _converterSystem.OnConvertCompleted += OnWorkFinished;
        }

        private void OnWorkFinished()
        {
            _handler.StopWork();
        }

        private void OnWorkStarted()
        {
            _handler.StartWork();
        }

        public void Dispose()
        {
            _converterSystem.OnConvertStarted -= OnWorkStarted;
            _converterSystem.OnConvertCompleted -= OnWorkFinished;
        }
    }
}
