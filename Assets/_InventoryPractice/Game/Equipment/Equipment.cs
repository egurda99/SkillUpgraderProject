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
        public event Action<EquipType, InventoryItem, int> OnUnEquipItem;
        public event Action<EquipType, InventoryItem, int> OnDropItem;

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
            var limit = GetSlotLimit(type);

            if (!_equippedItems.TryGetValue(type, out var list))
            {
                list = new List<InventoryItem>(new InventoryItem[limit]);
                _equippedItems[type] = list;
            }

            // Если уже экипирован — ничего не делаем
            if (list.Contains(item))
                return;

            // Найти первый пустой слот
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                {
                    list[i] = item;
                    OnEquipItem?.Invoke(type, item);
                    return;
                }
            }

            // Нет свободных — заменим первый
            var removed = list[0];
            list[0] = item;

            OnUnEquipItem?.Invoke(type, removed, 0);
            OnEquipItem?.Invoke(type, item);
        }

        public void Unequip(InventoryItem item)
        {
            foreach (var (type, list) in _equippedItems)
            {
                var index = list.IndexOf(item);
                if (index >= 0)
                {
                    list[index] = null;
                    OnUnEquipItem?.Invoke(type, item, index);
                    return;
                }
            }
        }

        public void DropItemFromEquipped(InventoryItem item)
        {
            foreach (var (type, list) in _equippedItems)
            {
                var index = list.IndexOf(item);
                if (index >= 0)
                {
                    list[index] = null;
                    OnDropItem?.Invoke(type, item, index);
                    return;
                }
            }
        }

        public List<InventoryItem> GetEquippedItems(EquipType type)
        {
            return _equippedItems.TryGetValue(type, out var list)
                ? new List<InventoryItem>(list)
                : new List<InventoryItem>();
        }

        public void SetSlotLimit(EquipType type, int limit)
        {
            _slotLimits[type] = limit;

            if (_equippedItems.TryGetValue(type, out var list))
            {
                if (list.Count > limit)
                {
                    for (var i = list.Count - 1; i >= limit; i--)
                    {
                        var removed = list[i];
                        list.RemoveAt(i);
                        if (removed != null)
                            OnUnEquipItem?.Invoke(type, removed, i);
                    }
                }
                else if (list.Count < limit)
                {
                    while (list.Count < limit)
                        list.Add(null);
                }
            }
        }

        public int GetSlotLimit(EquipType type)
        {
            return _slotLimits.TryGetValue(type, out var limit) ? limit : 0;
        }
    }
}
