namespace Game.Tutorial
{
    public sealed class WelcomePresenter
    {
        private readonly WelcomeConfig _welcomeConfig;

        private readonly WelcomeView _view;
        private readonly WelcomeStepController _welcomeStepController;


        public WelcomePresenter(WelcomeConfig welcomeConfig, WelcomeView view,
            WelcomeStepController welcomeStepController)
        {
            _welcomeConfig = welcomeConfig;
            _view = view;
            _welcomeStepController = welcomeStepController;
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
            _welcomeStepController.OnPopupClicked();
        }
    }
}
