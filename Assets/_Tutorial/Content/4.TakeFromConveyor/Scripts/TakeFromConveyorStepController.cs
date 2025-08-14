using _UpgradePractice.Scripts;
using Game.Tutorial.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class TakeFromConveyorStepController : TutorialStepControllerBase
    {
        private VisualZoneManager _visualZoneManager;

        private NavigationManager _navigationManager;


        [SerializeField] private TakeFromConveyorConfig _config;

        [SerializeField] private Transform _panelContainer;
        [SerializeField] private Transform _targetPosition;


        private ConverterInstaller _converterInstaller;
        private TakeFromConveyorPanelShower _takeFromConveyorPanelShower;


        [Inject]
        public void Construct(NavigationManager navigationManager, VisualZoneManager visualZoneManager,
            ConverterInstaller converterInstaller)
        {
            _navigationManager = navigationManager;
            _visualZoneManager = visualZoneManager;
            _converterInstaller = converterInstaller;

            _takeFromConveyorPanelShower = _config.TakeFromConveyorPanelShower;

            _takeFromConveyorPanelShower.Init(_config);
        }

        private void OnConverterOutputChanged(int obj)
        {
            //Убираем указатель
            _visualZoneManager.HideZone();
            _navigationManager.Stop();

            //Убираем квест из UI:
            _takeFromConveyorPanelShower.Hide();

            NotifyAboutCompleteAndMoveNext();
        }


        protected override void OnStart()
        {
            _converterInstaller.System.OnOutputChanged += OnConverterOutputChanged;

            //Показываем указатель:
            var targetPosition = _targetPosition.position;
            _visualZoneManager.ShowRectangleZone(targetPosition, Quaternion.Euler(90f, 0f, 0f));
            _navigationManager.StartLookAt(targetPosition);

            //Показываем квест в UI:
            _takeFromConveyorPanelShower.Show(_panelContainer);
        }

        protected override void OnStop()
        {
            base.OnStop();
            _converterInstaller.System.OnInputChanged -= OnConverterOutputChanged;
        }
    }
}
