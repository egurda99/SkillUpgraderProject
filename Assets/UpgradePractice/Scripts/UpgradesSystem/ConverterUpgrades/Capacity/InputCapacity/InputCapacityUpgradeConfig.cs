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

        public override float GetStatValue(int level)
        {
            if (level >= 1 && level <= MaxLevel)
            {
                return CapacityTable.GetCapacity(level);
            }

            return CapacityTable.GetCapacity(MaxLevel);
        }


        protected override void OnValidate()
        {
            base.OnValidate();
            CapacityTable.OnValidate(MaxLevel);
        }
    }
}
