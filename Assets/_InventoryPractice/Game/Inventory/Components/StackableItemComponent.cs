using System;
using UnityEngine;

namespace InventoryPractice
{
    [Serializable]
    public sealed class StackableItemComponent : IItemComponent
    {
        [SerializeField] private int _stackSize;
        [SerializeField] private int _value;

        public int StackSize => _stackSize;

        public int Value => _value;


        public bool IsFull
        {
            get { return Value >= StackSize; }
        }

        public IItemComponent Clone()
        {
            return new StackableItemComponent
            {
                _stackSize = StackSize,
                _value = Value
            };
        }

        public void AddValue(int amount)
        {
            _value += amount;
        }


        public void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, StackSize);
            _value = value;
        }

        public void DecreaseValue(int value)
        {
            _value -= value;
        }
    }
}
