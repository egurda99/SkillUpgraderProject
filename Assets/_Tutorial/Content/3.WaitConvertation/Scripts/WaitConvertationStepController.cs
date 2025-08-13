using _UpgradePractice.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class WaitConvertationStepController : TutorialStepControllerBase
    {
        [SerializeField] private WaitConvertationConfig _config;

        [SerializeField] private WaitConvertationPanelShower _waitConvertationPanelShower;

        [SerializeField] private Transform _panelContainer;
        private ConverterInstaller _converterInstaller;


        [Inject]
        public void Construct(ConverterInstaller converterInstaller)
        {
            _converterInstaller = converterInstaller;

            _waitConvertationPanelShower.Init(_config);
        }

        private void OnConvertationFinished(int obj)
        {
            //Убираем указатель
            //Убираем квест из UI:
            _waitConvertationPanelShower.Hide();

            NotifyAboutCompleteAndMoveNext();
        }


        protected override void OnStart()
        {
            //Показываем квест в UI:
            _converterInstaller.System.OnOutputChanged += OnConvertationFinished;

            _waitConvertationPanelShower.Show(_panelContainer);
        }

        protected override void OnStop()
        {
            base.OnStop();
            _converterInstaller.System.OnOutputChanged -= OnConvertationFinished;
        }
    }
}
