using MyCodeBase;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class WelcomeStepController : TutorialStepControllerBase
    {
        [SerializeField] private WelcomeConfig _config;


        private PopupManager _popupManager;
        private Popup _popup;

        [Inject]
        public void Construct(PopupManager popupManager)
        {
            _popupManager = popupManager;
        }

        protected override void OnStart()
        {
            _popup = _popupManager.FindPopup(_config.PopupName);
            _popup.OnPopupHided += OnPopupHided;

            if (_popup is WelcomePopup welcomePopup)
            {
                welcomePopup.Init(_config);
                _popupManager.ShowPopup(_config.PopupName);
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            _popup.OnPopupHided -= OnPopupHided;
        }


        public void OnPopupHided()
        {
            NotifyAboutCompleteAndMoveNext();
        }
    }
}
