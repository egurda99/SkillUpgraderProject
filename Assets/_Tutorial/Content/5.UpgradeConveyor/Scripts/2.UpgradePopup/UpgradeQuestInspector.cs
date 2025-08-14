using System;
using _UpgradePractice.Scripts;

namespace Game.Tutorial
{
    public sealed class UpgradeQuestInspector
    {
        private readonly UpgradeConveyorConfig _config;

        private readonly UpgradesManager _upgradesManager;

        private Upgrade _targetUpgrade;

        private Action _callback;

        public UpgradeQuestInspector(UpgradeConveyorConfig config, UpgradesManager upgradesManager)
        {
            _config = config;
            _upgradesManager = upgradesManager;
        }

        public void Inspect(Action callback)
        {
            _callback = callback;
            _targetUpgrade = _upgradesManager.GetUpgrade(_config.UpgradeConfig.Id);
            _targetUpgrade.OnLevelUp += OnLevelUp;
        }

        private void OnLevelUp(int currentLevel)
        {
            if (currentLevel < _config.TargetLevel)
            {
                return;
            }

            _targetUpgrade.OnLevelUp -= OnLevelUp;
            _targetUpgrade = null;
            _callback?.Invoke();
        }
    }
}
