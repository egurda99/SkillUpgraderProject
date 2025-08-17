using System;
using System.Reflection;
using InventoryPractice;

namespace TestsPractice
{
    [Serializable]
    public sealed class EquipableItemComponentDebug : EquipableItemComponent
    {
        public EquipableItemComponentDebug(
            EquipType equipType,
            int healthValue = 0,
            int powerValue = 0,
            int agilityValue = 0,
            int armorValue = 0)
        {
            Set("_equipType", equipType);
            Set("_healthValue", healthValue);
            Set("_powerValue", powerValue);
            Set("_aguilityValue", agilityValue);
            Set("_armorValue", armorValue);
        }

        private void Set(string fieldName, object value)
        {
            var field = typeof(EquipableItemComponent).GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(this, value);
        }

        public override IItemComponent Clone()
        {
            return new EquipableItemComponentDebug(
                EquipType,
                HealthValue,
                PowerValue,
                AguilityValue,
                ArmorValue
            );
        }
    }
}
