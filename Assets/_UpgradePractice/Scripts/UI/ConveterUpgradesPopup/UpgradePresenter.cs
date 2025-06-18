namespace _UpgradePractice.Scripts
{
    public sealed class UpgradePresenter
    {
        private readonly UpgradeConfig _upgradeConfig;

        private readonly IUpgradeView _view;

        private readonly UpgradesManager _upgradeManager;

        private Upgrade _currentUpgrade;

        public UpgradePresenter(UpgradeConfig upgradeConfig, IUpgradeView view, UpgradesManager upgradeManager)
        {
            _upgradeConfig = upgradeConfig;
            _view = view;
            _upgradeManager = upgradeManager;
        }

        public void Start()
        {
            _currentUpgrade = _upgradeManager.GetUpgrade(_upgradeConfig.Id);
            _view.SetTitleText(_upgradeConfig.Metadata.Title);
            _view.SetDescriptionText(_upgradeConfig.Metadata.Decription);
            _view.SetIconImage(_upgradeConfig.Metadata.Icon);
            _view.SetPrice(_currentUpgrade.NextPrice.ToString());
            UpdateStatInfo();

            _view.SetButtonInteractable(_upgradeManager.CanLevelUp(_upgradeConfig.Id));

            _view.AddButtonListener(OnBuyClicked);
            _upgradeManager.OnLevelUp += OnLevelUp;
        }


        public void Stop()
        {
            _upgradeManager.OnLevelUp -= OnLevelUp;

            _view.RemoveButtonListener(OnBuyClicked);
        }

        private void OnBuyClicked()
        {
            if (_upgradeManager.CanLevelUp(_upgradeConfig.Id))
            {
                _upgradeManager.LevelUp(_upgradeConfig.Id);
            }
        }


        private void UpdateStatInfo()
        {
            _view.SetCurrentStatText("Current: " + _upgradeConfig.GetStatValue(_currentUpgrade.Level));

            if (_currentUpgrade.IsMaxLevel)
            {
                _view.SetUpgradedStatText("Max level reached ");
            }
            else
            {
                _view.SetUpgradedStatText("Upgraded: " + _upgradeConfig.GetStatValue(_currentUpgrade.Level + 1));
            }
        }

        private void OnLevelUp(Upgrade upgrade)
        {
            if (upgrade.Id != _upgradeConfig.Id)
                return;

            _view.SetPrice(_currentUpgrade.NextPrice.ToString());
            UpdateStatInfo();

            _view.SetButtonInteractable(_upgradeManager.CanLevelUp(_upgradeConfig.Id));
        }
    }
}
