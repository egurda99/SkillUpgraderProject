using MyCodeBase;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Tutorial
{
    public sealed class FinalPopupStepController : TutorialStepControllerBase
    {
        [SerializeField] private FinalStepConfig _config;
        [SerializeField] private Button _closeButton;


        private PopupManager _popupManager;


        [Inject]
        public void Construct(PopupManager popupManager)
        {
            _popupManager = popupManager;
        }

        protected override void OnStart()
        {
            _closeButton.onClick.AddListener(OnCloseClicked);
        }

        private void OnCloseClicked()
        {
            _popupManager.HidePopup(_config.PopupName);
            _closeButton.onClick.RemoveListener(OnCloseClicked);

            NotifyAboutCompleteAndMoveNext();
        }


        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
