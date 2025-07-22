using System;

namespace _UpgradePractice.Scripts
{
    public sealed class ConverterVisualInstaller : IDisposable
    {
        private readonly ConverterView _view;
        private readonly ConverterSystem _system;


        private InputZoneVisualAdapter _inputZoneVisualAdapter;
        private OutputZoneVisualAdapter _outputZoneVisualAdapter;
        private ProgressBarAdapter _progressBarAdapter;
        private WorkerAnimationAdapter _workerAnimationAdapter;

        public ConverterVisualInstaller(ConverterView view, ConverterSystem system)
        {
            _view = view;
            _system = system;

            Init();
        }

        private void Init()
        {
            _inputZoneVisualAdapter = new InputZoneVisualAdapter(_system, _view.InputZoneVisual);
            _outputZoneVisualAdapter = new OutputZoneVisualAdapter(_system, _view.OutputZoneVisual);
            _progressBarAdapter = new ProgressBarAdapter(_view.ProgressBar, _system);
            _workerAnimationAdapter =
                new WorkerAnimationAdapter(new WorkerAnimationHandler(_view.WorkerAnimator), _system);
        }

        public void Dispose()
        {
            _inputZoneVisualAdapter.Dispose();
            _outputZoneVisualAdapter.Dispose();
            _progressBarAdapter.Dispose();
            _workerAnimationAdapter.Dispose();
        }
    }
}