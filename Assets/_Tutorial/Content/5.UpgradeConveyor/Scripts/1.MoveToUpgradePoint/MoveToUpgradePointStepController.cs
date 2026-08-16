using _UpgradePractice.Scripts;
using Game.Tutorial.Gameplay;
using MyCodeBase;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class MoveToUpgradePointStepController : TutorialStepControllerBase
    {
        private VisualZoneManager _visualZoneManager;

        private NavigationManager _navigationManager;


        [SerializeField] private UpgradeConveyorConfig _config;

        [SerializeField] private Transform _panelContainer;
        [SerializeField] private Transform _targetPosition;


        private UpgradeConveyorPanelShower _upgradeConveyorPanelShower;
        private UpgradeTriggerPoint _upgradeTriggerPoint;
        private PlaceTriggerPoint _triggerPoint;
        private PopupManager _popupManager;


        [Inject]
        public void Construct(NavigationManager navigationManager, VisualZoneManager visualZoneManager,
            PopupManager popupManager)
        {
            _navigationManager = navigationManager;
            _visualZoneManager = visualZoneManager;
            _popupManager = popupManager;


            _upgradeConveyorPanelShower = _config.UpgradeConveyorPanelShower;

            _upgradeConveyorPanelShower.Init(_config);
        }

        protected override void OnStart()
        {
            var placeTrigger = Instantiate(_config.PlaceTriggerPointPrefab, _targetPosition.position,
                Quaternion.identity);

            if (placeTrigger.TryGetComponent(out PlaceTriggerPoint triggerPoint))
            {
                _triggerPoint = triggerPoint;
                _triggerPoint.OnPlaceVisited += OnPlaceVisited;
            }


            //���������� ���������:
            var targetPosition = _targetPosition.position;
            _visualZoneManager.ShowCircleZone(targetPosition, Quaternion.Euler(90f, 0f, 0f));
            _navigationManager.StartLookAt(targetPosition);

            //���������� ����� � UI:
            _upgradeConveyorPanelShower.Show(_panelContainer);
        }

        private void OnPlaceVisited()
        {
            _visualZoneManager.HideZone();
            _navigationManager.Stop();

            _upgradeConveyorPanelShower.Hide();
            _popupManager.ShowPopup(_config.PopupName);
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (_triggerPoint != null)
            {
                _triggerPoint.OnPlaceVisited -= OnPlaceVisited;
                Destroy(_triggerPoint.gameObject);
                _triggerPoint = null;
            }

            _visualZoneManager.HideZone();
            _navigationManager.Stop();
            _upgradeConveyorPanelShower.Hide();
        }
    }
}
