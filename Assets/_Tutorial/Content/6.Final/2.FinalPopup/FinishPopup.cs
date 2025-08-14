using MyCodeBase;
using UnityEngine;

namespace Game.Tutorial
{
    public sealed class FinishPopup : Popup
    {
        [SerializeField] private WelcomeView _welcomeView;

        private FinishPopupPresenter _finishPopupPresenter;
        private FinalStepConfig _config;

        protected override void OnShow()
        {
            base.OnShow();
            ShowPopup();
        }

        public void Init(FinalStepConfig config)
        {
            _config = config;
            _finishPopupPresenter = new FinishPopupPresenter(_config, _welcomeView);
        }

        private void ShowPopup()
        {
            _finishPopupPresenter.Start();
        }

        protected override void OnHide()
        {
            base.OnHide();
        }
    }
}
