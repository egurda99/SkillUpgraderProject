using _UpgradePractice.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class WaitConvertationStepController : TutorialStepControllerBase
    {
        [SerializeField] private WaitConvertationConfig _config;
        [SerializeField] private Transform _panelContainer;

        private ConverterInstaller _converterInstaller;
        private WaitConvertationPanelShower _waitConvertationPanelShower;


        [Inject]
        public void Construct(ConverterInstaller converterInstaller)
        {
            _converterInstaller = converterInstaller;

            _waitConvertationPanelShower = _config.WaitConvertationPanelShower;
            _waitConvertationPanelShower.Init(_config);
        }

        private void OnConvertationFinished(int obj)
        {
            //������� ���������
            //������� ����� �� UI:
            _waitConvertationPanelShower.Hide();

            NotifyAboutCompleteAndMoveNext();
        }


        protected override void OnStart()
        {
            //���������� ����� � UI:
            _converterInstaller.System.OnOutputChanged += OnConvertationFinished;

            _waitConvertationPanelShower.Show(_panelContainer);
        }

        protected override void OnStop()
        {
            base.OnStop();

            _converterInstaller.System.OnOutputChanged -= OnConvertationFinished;
            _waitConvertationPanelShower.Hide();
        }
    }
}
