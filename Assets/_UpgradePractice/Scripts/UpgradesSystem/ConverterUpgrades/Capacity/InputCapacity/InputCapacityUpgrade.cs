using UnityEngine;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class InputCapacityUpgrade : Upgrade
    {
        private readonly InputCapacityUpgradeConfig _inputCapacityUpgradeConfig;
        private ConverterData _converterData;

        public InputCapacityUpgrade(InputCapacityUpgradeConfig config) : base(config)
        {
            _inputCapacityUpgradeConfig = config;
        }

        [Inject]
        public void Construct(ConverterInstaller converterInstaller)
        {
            _converterData = converterInstaller.View.Data;
            var capacity = _inputCapacityUpgradeConfig.CapacityTable.GetCapacity(Level);
            _converterData.SetInputZoneCapacity(capacity);
        }

        protected override void OnUpgrade()
        {
            var capacity = _inputCapacityUpgradeConfig.CapacityTable.GetCapacity(Level);
            _converterData.SetInputZoneCapacity(capacity);
            Debug.Log($"<color=blue>Input capacity Upgraded. New capacity: {capacity}</color>");
        }
    }
}
