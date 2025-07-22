using UnityEngine;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class OutputCapacityUpgrade : Upgrade
    {
        private readonly OutputCapacityUpgradeConfig _outputCapacityUpgradeConfig;
        private ConverterData _converterData;

        public OutputCapacityUpgrade(OutputCapacityUpgradeConfig config) : base(config)
        {
            _outputCapacityUpgradeConfig = config;
        }

        [Inject]
        public void Construct(ConverterInstaller converterInstaller)
        {
            _converterData = converterInstaller.View.Data;
            var capacity = _outputCapacityUpgradeConfig.CapacityTable.GetCapacity(Level);
            _converterData.SetOutputZoneCapacity(capacity);
        }

        protected override void OnUpgrade()
        {
            var capacity = _outputCapacityUpgradeConfig.CapacityTable.GetCapacity(Level);
            _converterData.SetOutputZoneCapacity(capacity);
            Debug.Log($"<color=blue>Output capacity Upgraded. New capacity: {capacity}</color>");
        }
    }
}
