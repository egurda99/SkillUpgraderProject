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
        public event Action<InventoryItem, int, EquipType> OnItemEquipByDragAndDrop;

        public event Action<EquipType, InventoryItem, int> OnUnEquipItemView;
        public event Action<EquipType, InventoryItem, int> OnEquipItemView;
        public event Action<EquipType, InventoryItem, int> OnUnEquipItem;
        public event Action<InventoryItem, int> OnUnEquipItemToConcreteSlot;
        public event Action<EquipType, InventoryItem, int> OnDropOutItem;

        public Equipment()
        {
            _slotLimits[EquipType.Helmet] = 1;
            _slotLimits[EquipType.Armor] = 1;
            _slotLimits[EquipType.Hand] = 2;
            _slotLimits[EquipType.Boots] = 1;
        }

        public void EquipItemFromDragAndDrop(InventoryItem item, int index, EquipType slotEquipType)
        {
            OnItemEquipByDragAndDrop?.Invoke(item, index, slotEquipType);
        }

        public void Equip(InventoryItem item, EquipType slotEquipType = EquipType.None, int index = 0)
        {
            if (item == null || !item.TryGetComponent(out EquipableItemComponent equipComponent))
                return;

            var type = equipComponent.EquipType;

            var itemType = equipComponent.EquipType;

            if (slotEquipType != EquipType.None && itemType != slotEquipType)
            {
                return;
            }


            var limit = GetSlotLimit(type);

            if (!_equippedItems.TryGetValue(type, out var list))
            {
                list = new List<InventoryItem>(new InventoryItem[limit]);
                _equippedItems[type] = list;
            }

            var currentIndex = list.IndexOf(item); // for change slots positions in one equipType
            if (currentIndex != -1)
            {
                // ≈сли больше одного слота Ч можно помен€ть местами
                if (limit > 1 && index != currentIndex && index >= 0 && index < limit)
                {
                    (list[currentIndex], list[index]) = (list[index], list[currentIndex]);

                    OnEquipItemView?.Invoke(type, list[index], index); // новый на новой позиции
                    OnEquipItemView?.Invoke(type, list[currentIndex], currentIndex); // старый на другой позиции
                }

                return;
            }


            // Ќайти первый пустой слот
            if (index == 0)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    if (list[i] == null)
                    {
                        list[i] = item;
                        OnEquipItem?.Invoke(type, item);
                        return;
                    }
                }
            }

            else
            {
                if (list[index] == null)
                {
                    list[index] = item;
                    OnEquipItem?.Invoke(type, item);
                    return;
                }

                var removedItem = list[index];
                list[index] = item;

                OnUnEquipItem?.Invoke(type, removedItem, 1);
                OnEquipItem?.Invoke(type, item);
                return;
            }


            // Ќет свободных Ч заменим первый
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

        public void UnEquipFromDrop(InventoryItem item)
        {
            foreach (var (type, list) in _equippedItems)
            {
                var index = list.IndexOf(item);
                if (index >= 0)
                {
                    list[index] = null;
                    OnUnEquipItemView?.Invoke(type, item, index); // inventory model dont react
                    return;
                }
            }
        }

        public void TryUnequipToSlot(InventoryItem item, int inventorySlotIndex)
        {
            foreach (var (type, list) in _equippedItems)
            {
                var index = list.IndexOf(item);
                if (index >= 0)
                {
                    OnUnEquipItemToConcreteSlot?.Invoke(item, inventorySlotIndex);
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
                    OnDropOutItem?.Invoke(type, item, index);
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
