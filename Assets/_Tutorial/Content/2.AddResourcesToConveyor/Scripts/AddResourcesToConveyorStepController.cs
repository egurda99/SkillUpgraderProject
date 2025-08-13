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

        [SerializeField] private MoveToConveyorPanelShower _moveToConveyorPanelShower;

        [SerializeField] private Transform _targetPosition;
        [SerializeField] private Transform _panelContainer;
        private ConverterInstaller _converterInstaller;


        [Inject]
        public void Construct(NavigationManager navigationManager, VisualZoneManager visualZoneManager,
            ConverterInstaller converterInstaller)
        {
            _navigationManager = navigationManager;
            _visualZoneManager = visualZoneManager;
            _converterInstaller = converterInstaller;

            _moveToConveyorPanelShower.Init(_config);
        }

        public override void Init()
        {
            base.Init();
            _converterInstaller.System.OnInputChanged += OnConverterVisited;
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
            //Показываем указатель:
            var targetPosition = _targetPosition.position;
            _visualZoneManager.ShowZone(targetPosition, Quaternion.Euler(90f, 0f, 0f));
            _navigationManager.StartLookAt(targetPosition);

            //Показываем квест в UI:
            _moveToConveyorPanelShower.Show(_panelContainer);
        }
    }
}
