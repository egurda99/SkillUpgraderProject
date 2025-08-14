using _Tutorial.Core;
using _UpgradePractice.Scripts;
using MyCodeBase;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Tutorial
{
    public sealed class UpgradeConveyorPopupStepController : TutorialStepControllerBase
    {
        [SerializeField] private UpgradeConveyorConfig _config;
        [SerializeField] private TutorialCursor _cursorUpgrade;
        [SerializeField] private TutorialCursor _cursorCloseButton;
        [SerializeField] private Button _closeButton;


        private PopupManager _popupManager;
        private UpgradeQuestInspector _upgradeQuestInspector;
        private UpgradesManager _upgradeManager;

        private void Awake()
        {
            _cursorUpgrade.gameObject.SetActive(false);
            _cursorCloseButton.gameObject.SetActive(false);
            _closeButton.interactable = false;
        }

        [Inject]
        public void Construct(PopupManager popupManager, UpgradesManager upgradesManager)
        {
            _popupManager = popupManager;
            _upgradeManager = upgradesManager;
        }

        protected override void OnStart()
        {
            _closeButton.onClick.AddListener(OnCloseClicked);
            _upgradeQuestInspector = new UpgradeQuestInspector(_config, _upgradeManager);
            _upgradeQuestInspector.Inspect(OnUpgraded);

            _cursorUpgrade.gameObject.SetActive(true);
        }

        private void OnCloseClicked()
        {
            _popupManager.HidePopup(_config.PopupName);
            _closeButton.onClick.RemoveListener(OnCloseClicked);

            NotifyAboutMoveNext();
        }

        private void OnUpgraded()
        {
            _cursorUpgrade.gameObject.SetActive(false);
            _popupManager.ShowPopup(_config.PopupName);

            _closeButton.interactable = true;
            _cursorCloseButton.gameObject.SetActive(true);
            NotifyAboutComplete();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
