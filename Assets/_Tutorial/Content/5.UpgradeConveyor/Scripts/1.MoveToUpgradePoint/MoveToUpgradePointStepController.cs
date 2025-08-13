using _UpgradePractice.Scripts;
using Game.Tutorial.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class MoveToUpgradePointStepController : TutorialStepControllerBase
    {
        private VisualZoneManager _visualZoneManager;

        private NavigationManager _navigationManager;


        [SerializeField] private UpgradeConveyorConfig _config;


        private UpgradeConveyorPanelShower _upgradeConveyorPanelShower;
        private UpgradeTriggerPoint _upgradeTriggerPoint;
        private PlaceTriggerPoint _triggerPoint;


        [Inject]
        public void Construct(NavigationManager navigationManager, VisualZoneManager visualZoneManager,
            UpgradeTriggerPoint upgradetriggerPoint)
        {
            _navigationManager = navigationManager;
            _visualZoneManager = visualZoneManager;
            _upgradeTriggerPoint = upgradetriggerPoint;


            _upgradeConveyorPanelShower = _config.UpgradeConveyorPanelShower;

            _upgradeConveyorPanelShower.Init(_config);
        }

        protected override void OnStart()
        {
            if (!IsStepFinished())
            {
                _upgradeTriggerPoint.gameObject.SetActive(false);
            }

            var placeTrigger = Instantiate(_config.PlaceTriggerPointPrefab, _config.TargetPosition.position,
                Quaternion.identity);

            if (placeTrigger.TryGetComponent(out PlaceTriggerPoint triggerPoint))
            {
                _triggerPoint = triggerPoint;
                _triggerPoint.OnPlaceVisited += OnPlaceVisited;
            }


            //Показываем указатель:
            var targetPosition = _config.TargetPosition.position;
            _visualZoneManager.ShowCircleZone(targetPosition, Quaternion.Euler(90f, 0f, 0f));
            _navigationManager.StartLookAt(targetPosition);

            //Показываем квест в UI:
            _upgradeConveyorPanelShower.Show(_config.PanelContainer);
        }

        private void OnPlaceVisited()
        {
            _visualZoneManager.HideZone();
            _navigationManager.Stop();

            _upgradeConveyorPanelShower.Hide();
        }

        protected override void OnStop()
        {
            base.OnStop();
            _triggerPoint.OnPlaceVisited -= OnPlaceVisited;
        }
    }
}
