using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [CreateAssetMenu(
        fileName = "TimeConvertationConfig",
        menuName = "Configs/Upgrade/New TimeConvertationUpgradeConfig"
    )]
    public class TimeConvertationUpgradeConfig : UpgradeConfig
    {
        public TimeConvertationTable TimeConvertationTable;

        public override Upgrade Create()
        {
            return new TimeConvertationUpgrade(this);
        }

        public override float GetStatValue(int level)
        {
            if (level >= 1 && level <= MaxLevel)
            {
                return TimeConvertationTable.GetTime(level);
            }

            return TimeConvertationTable.GetTime(MaxLevel);
        }


        protected override void OnValidate()
        {
            base.OnValidate();
            TimeConvertationTable.OnValidate(MaxLevel);
        }
    }
}
