using System;
using UnityEngine;

namespace InventoryPractice
{
    [Serializable]
    public class EquipableItemComponent : IItemComponent
    {
        [SerializeField] private EquipType _equipType;
        [SerializeField] private int _healthValue;
        [SerializeField] private int _powerValue;
        [SerializeField] private int _aguilityValue;
        [SerializeField] private int _armorValue;

        public EquipType EquipType => _equipType;

        public int HealthValue => _healthValue;

        public int PowerValue => _powerValue;

        public int AguilityValue => _aguilityValue;

        public int ArmorValue => _armorValue;


        public virtual IItemComponent Clone()
        {
            return new EquipableItemComponent
            {
                _equipType = EquipType,
                _healthValue = HealthValue,
                _aguilityValue = AguilityValue,
                _powerValue = PowerValue,
                _armorValue = ArmorValue
            };
        }
    }
}
