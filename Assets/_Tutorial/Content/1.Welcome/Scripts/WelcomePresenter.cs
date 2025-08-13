namespace Game.Tutorial
{
    public sealed class WelcomePresenter
    {
        private readonly WelcomeConfig _welcomeConfig;

        private readonly WelcomeView _view;
        private readonly WelcomePopup _popup;


        public WelcomePresenter(WelcomeConfig welcomeConfig, WelcomeView view,
            WelcomePopup popup)
        {
            _welcomeConfig = welcomeConfig;
            _view = view;
            _popup = popup;
        }

        public void Start()
        {
            _view.SetTitle(_welcomeConfig.Title);
            _view.SetDescription(_welcomeConfig.Description);

            _view.AddButtonListener(OnPopupClicked);
        }

        public void Stop()
        {
            _view.RemoveButtonListener(OnPopupClicked);
        }

        private void OnPopupClicked()
        {
            _popup.HideRequested();
        }
    }
}
