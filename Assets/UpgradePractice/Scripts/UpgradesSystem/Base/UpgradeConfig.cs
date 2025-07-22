using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        public string Id;
        public int MaxLevel;
        public UpgradePriceTable PriceTable;


        [SerializeField] private UpgradeMetaData _metadata;

        public UpgradeMetaData Metadata => _metadata;

        public abstract Upgrade Create();

        public abstract float GetStatValue(int level);

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
