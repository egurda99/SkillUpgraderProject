using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        public string Id;
        public int MaxLevel;
        public UpgradePriceTable PriceTable;

        public abstract Upgrade Create();

        public int GetNextPrice(int level)
        {
            return PriceTable.GetPrice(level);
        }

        protected virtual void OnValidate()
        {
            PriceTable.OnValidate(MaxLevel);
        }
    }
}