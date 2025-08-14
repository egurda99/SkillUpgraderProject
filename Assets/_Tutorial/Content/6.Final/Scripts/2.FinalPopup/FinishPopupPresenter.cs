namespace Game.Tutorial
{
    public sealed class FinishPopupPresenter
    {
        private readonly FinalStepConfig _finalStepConfig;

        private readonly WelcomeView _view;


        public FinishPopupPresenter(FinalStepConfig finalStepConfig, WelcomeView view)
        {
            _finalStepConfig = finalStepConfig;
            _view = view;
        }

        public void Start()
        {
            _view.SetTitle(_finalStepConfig.TitlePopup);
            _view.SetDescription(_finalStepConfig.DescriptionPopup);
        }
    }
}
