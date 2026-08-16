using _UpgradePractice.Scripts;
using Game.Tutorial.Gameplay;
using MyCodeBase;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class FinalStepController : TutorialStepControllerBase
    {
        private VisualZoneManager _visualZoneManager;

        private NavigationManager _navigationManager;


        [SerializeField] private FinalStepConfig _config;

        [SerializeField] private Transform _panelContainer;
        [SerializeField] private Transform _targetPosition;


        private FinalPanelShower _finalPanelShower;
        private PlaceTriggerPoint _triggerPoint;
        private PopupManager _popupManager;
        private Popup _popup;


        [Inject]
        public void Construct(NavigationManager navigationManager, VisualZoneManager visualZoneManager,
            PopupManager popupManager)
        {
            _navigationManager = navigationManager;
            _visualZoneManager = visualZoneManager;
            _popupManager = popupManager;


            _finalPanelShower = _config.FinalPanelShower;

            _finalPanelShower.Init(_config);
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
            _finalPanelShower.Show(_panelContainer);
        }

        private void OnPlaceVisited()
        {
            _visualZoneManager.HideZone();
            _navigationManager.Stop();

            _finalPanelShower.Hide();

            _popup = _popupManager.FindPopup(_config.PopupName);

            if (_popup is FinishPopup finishPopup)
            {
                finishPopup.Init(_config);
                _popupManager.ShowPopup(_config.PopupName);
            }

            NotifyAboutComplete();


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
            _finalPanelShower.Hide();
        }
    }
}
