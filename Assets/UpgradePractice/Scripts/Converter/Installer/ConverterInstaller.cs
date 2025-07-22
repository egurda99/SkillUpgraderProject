using System;
using MyTimer;
using UnityEngine;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class ConverterInstaller : MonoBehaviour, IDisposable
    {
        [SerializeField] private ConverterView _view;

        private ConverterSystem _system;
        public ConverterView View => _view;
        public ConverterSystem System => _system;

        private ConverterVisualInstaller _converterVisualInstaller;
        private Timer _timer;


        [Inject]
        private void Construct(Timer timer)
        {
            _timer = timer;
            _system = new ConverterSystem(_view.Data, _timer);

            _converterVisualInstaller = new ConverterVisualInstaller(_view, _system);
        }

        private void Update()
        {
            if (_system == null)
            {
                return;
            }

            _system.OnUpdate();
        }

        public void Dispose()
        {
            _converterVisualInstaller.Dispose();
        }
    }
}
