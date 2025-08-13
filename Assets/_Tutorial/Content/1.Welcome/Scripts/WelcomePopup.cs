using MyCodeBase;
using UnityEngine;

namespace Game.Tutorial
{
    public sealed class WelcomePopup : Popup
    {
        [SerializeField] private WelcomeView _welcomeView;

        private WelcomePresenter _welcomePresenter;
        private WelcomeConfig _config;

        protected override void OnShow()
        {
            base.OnShow();
            ShowPopup();
        }

        public void Init(WelcomeConfig config)
        {
            _config = config;
            _welcomePresenter = new WelcomePresenter(_config, _welcomeView, this);
        }

        private void ShowPopup()
        {
            _welcomePresenter.Start();
        }

        protected override void OnHide()
        {
            base.OnHide();
            _welcomePresenter.Stop();
        }
    }
}
