using MyCodeBase;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class WelcomeStepController : TutorialStepControllerBase
    {
        [SerializeField] private WelcomeConfig _config;
        [SerializeField] private WelcomeView _welcomeView;


        private WelcomePresenter _welcomePresenter;
        private PopupManager _popupManager;

        [Inject]
        public void Construct(PopupManager popupManager)
        {
            _popupManager = popupManager;
            _welcomePresenter = new WelcomePresenter(_config, _welcomeView, this);
        }

        protected override void OnStart()
        {
            _welcomePresenter.Start();
            _popupManager.ShowPopup(_config.PopupName);
        }

        public void OnPopupClicked()
        {
            NotifyAboutCompleteAndMoveNext();
            _welcomePresenter.Stop();
            _popupManager.HidePopup(_config.PopupName);
        }
    }
}
