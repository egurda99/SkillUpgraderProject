using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [CreateAssetMenu(
        fileName = "InputCapacityUpgradeConfig",
        menuName = "Configs/Upgrade/New InputCapacityUpgradeConfig"
    )]
    public class InputCapacityUpgradeConfig : UpgradeConfig
    {
        public CapacityTable CapacityTable;

        public override Upgrade Create()
        {
            return new InputCapacityUpgrade(this);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            CapacityTable.OnValidate(MaxLevel);
        }
    }
}
