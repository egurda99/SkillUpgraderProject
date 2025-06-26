using System;
using System.Collections.Generic;

namespace InventoryPractice
{
    public sealed class Equipment
    {
        private readonly Dictionary<EquipType, List<InventoryItem>> _equippedItems = new();
        private readonly Dictionary<EquipType, int> _slotLimits = new();

        public IReadOnlyDictionary<EquipType, List<InventoryItem>> EquippedItems => _equippedItems;

        public event Action<EquipType, InventoryItem> OnEquipItem;
        public event Action<EquipType, InventoryItem> OnUnEquipItem;

        public Equipment()
        {
            _slotLimits[EquipType.Helmet] = 1;
            _slotLimits[EquipType.Armor] = 1;
            _slotLimits[EquipType.Hand] = 2;
            _slotLimits[EquipType.Boots] = 1;
        }

        public void Equip(InventoryItem item)
        {
            if (item == null || !item.TryGetComponent(out EquipableItemComponent equipComponent))
                return;

            var type = equipComponent.EquipType;

            if (!_slotLimits.TryGetValue(type, out var limit))
                return;

            if (!_equippedItems.TryGetValue(type, out var itemList))
            {
                itemList = new List<InventoryItem>();
                _equippedItems[type] = itemList;
            }

            if (itemList.Count >= limit)
            {
                var removedItem = itemList[0];
                itemList.RemoveAt(0);

                OnUnEquipItem?.Invoke(type, removedItem);
            }

            itemList.Add(item);
            OnEquipItem?.Invoke(type, item);
        }

        public void Unequip(EquipType type, InventoryItem item)
        {
            if (!_equippedItems.TryGetValue(type, out var itemList))
                return;

            if (itemList.Remove(item))
            {
                OnUnEquipItem?.Invoke(type, item);
                if (itemList.Count == 0)
                    _equippedItems.Remove(type);
            }
        }

        public void Unequip(InventoryItem item)
        {
            foreach (var kvp in _equippedItems)
            {
                var type = kvp.Key;
                var list = kvp.Value;

                if (list.Remove(item))
                {
                    OnUnEquipItem?.Invoke(type, item);
                    if (list.Count == 0)
                        _equippedItems.Remove(type);
                    break;
                }
            }
        }

        public List<InventoryItem> GetEquippedItems(EquipType type)
        {
            if (_equippedItems.TryGetValue(type, out var list))
                return list;
            return new List<InventoryItem>();
        }

        public void SetSlotLimit(EquipType type, int limit)
        {
            _slotLimits[type] = limit;
        }

        public int GetSlotLimit(EquipType type)
        {
            return _slotLimits[type];
        }
    }
}
