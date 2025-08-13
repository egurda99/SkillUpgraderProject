using _UpgradePractice.Scripts;
using Game.Tutorial.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class AddResourcesToConveyorStepController : TutorialStepControllerBase
    {
        private VisualZoneManager _visualZoneManager;

        private NavigationManager _navigationManager;


        [SerializeField] private AddResourcesToConveyorConfig _config;


        private ConverterInstaller _converterInstaller;
        private MoveToConveyorPanelShower _moveToConveyorPanelShower;


        [Inject]
        public void Construct(NavigationManager navigationManager, VisualZoneManager visualZoneManager,
            ConverterInstaller converterInstaller)
        {
            _navigationManager = navigationManager;
            _visualZoneManager = visualZoneManager;
            _converterInstaller = converterInstaller;

            _moveToConveyorPanelShower = _config.MoveToConveyorPanelShower;

            _moveToConveyorPanelShower.Init(_config);
        }

        private void OnConverterVisited(int obj)
        {
            //Убираем указатель
            _visualZoneManager.HideZone();
            _navigationManager.Stop();

            //Убираем квест из UI:
            _moveToConveyorPanelShower.Hide();

            NotifyAboutCompleteAndMoveNext();
        }


        protected override void OnStart()
        {
            _converterInstaller.System.OnInputChanged += OnConverterVisited;

            //Показываем указатель:
            var targetPosition = _config.TargetPosition.position;
            _visualZoneManager.ShowZone(targetPosition, Quaternion.Euler(90f, 0f, 0f));
            _navigationManager.StartLookAt(targetPosition);

            //Показываем квест в UI:
            _moveToConveyorPanelShower.Show(_config.PanelContainer);
        }

        protected override void OnStop()
        {
            base.OnStop();
            _converterInstaller.System.OnInputChanged -= OnConverterVisited;
        }
    }
}
