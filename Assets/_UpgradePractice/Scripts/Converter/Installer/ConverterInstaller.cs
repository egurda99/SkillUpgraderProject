using System;
using MyTimer;
using UnityEngine;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class ConverterInstaller : MonoBehaviour, IDisposable
    {
        [SerializeField] private ConverterView _view;
        private ConverterData _converterData;

        private ConverterSystem _system;
        public ConverterView View => _view;
        public ConverterSystem System => _system;

        private ConverterVisualInstaller _converterVisualInstaller;
        private Timer _timer;
        public ConverterData Data => _converterData;


        [Inject]
        private void Construct(Timer timer, ConverterDataService converterDataService)
        {
            _timer = timer;
            _converterData = converterDataService.ConverterData;

            _system = new ConverterSystem(_converterData, _timer);

            _converterVisualInstaller = new ConverterVisualInstaller(_view, _system);
        }

        private void Update()
        {
            _system.OnUpdate();
        }

        public void Dispose()
        {
            _converterVisualInstaller.Dispose();
        }
    }
}
