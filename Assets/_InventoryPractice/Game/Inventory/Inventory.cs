using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventoryPractice
{
    public sealed class Inventory
    {
        private int _slotsLimit = 20;
        private int _weightLimit = 100;

        [ShowInInspector] [ReadOnly] private List<InventoryItem> _items = new();

        public List<InventoryItem> Items => _items;

        public int SlotsLimit => _slotsLimit;
        public int UsedWeight => _items.Sum(i => i.Weight);
        public bool HasFreeSlot => _items.Count < _slotsLimit;

        public int WeightLimit => _weightLimit;

        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem, int> OnItemsAdded;

        public event Action<InventoryItem> OnItemRemoved;
        public event Action<InventoryItem, int> OnItemsRemoved;

        public event Action<InventoryItem> OnItemConsumed;
        public event Action<InventoryItem> OnItemEquipped;

        public event Action OnInventoryListChanged;

        public event Action<int> OnWeightChanged;


        private int _currentWeight;

        public int CurrentWeight => _currentWeight;

        public void Init(int slotsLimit, int weightLimit)
        {
            _slotsLimit = slotsLimit;
            _weightLimit = weightLimit;
        }

        public void AddItem(InventoryItem item)
        {
            _items.Add(item);

            OnInventoryListChanged?.Invoke();
        }

        public bool CanAddItem(InventoryItem item)
        {
            var requiredWeight = item.Weight;
            var isStackable = item.Flags.HasFlag(InventoryItemFlags.Stackable);

            if (!isStackable)
            {
                return HasFreeSlot && CanAddWeight(requiredWeight);
            }

            foreach (var i in _items)
            {
                if (i.Id == item.Id &&
                    i.TryGetComponent(out StackableItemComponent stack) &&
                    !stack.IsFull)
                {
                    return CanAddWeight(requiredWeight);
                }
            }

            return HasFreeSlot && CanAddWeight(requiredWeight);
        }

        [Button]
        public void AddItemToInventory(InventoryItemConfig itemConfig)
        {
            var item = itemConfig.PrototypeItem.Clone();

            if (CanAddItem(item))
            {
                OnItemAdded?.Invoke(item);
            }
        }

        [Button]
        public void AddItemToInventory(InventoryItem item)
        {
            if (CanAddItem(item))
            {
                OnItemAdded?.Invoke(item);
            }
        }

        [Button]
        public void AddItems(InventoryItemConfig itemConfig, int amount)
        {
            var item = itemConfig.PrototypeItem.Clone();

            var amountToAdd = GetMaxAddableAmount(item, amount);

            if (amountToAdd > 0)
            {
                OnItemsAdded?.Invoke(item, amountToAdd);
            }

            else
            {
                Debug.LogWarning("Not enough space in inventory");
            }
        }

        public bool CanAddWeight(int weight)
        {
            var requiredWeight = weight + _currentWeight;

            return requiredWeight <= _weightLimit;
        }


        public void RemoveItem(InventoryItem item)
        {
            _items.Remove(item);
            //  DecreaseWeight(item.Weight);
            OnInventoryListChanged?.Invoke();
        }

        public void RemoveItemSlot(InventoryItem item)
        {
            if (item != null)
            {
                OnItemRemoved?.Invoke(item);
            }
        }

        public void AddItemSlot(InventoryItem item)
        {
            if (CanAddItem(item))
            {
                OnItemAdded?.Invoke(item);
            }
        }

        [Button]
        public void RemoveItem(InventoryItemConfig itemConfig)
        {
            var itemId = itemConfig.PrototypeItem.Id;
            var lastItem = _items.LastOrDefault(i => i.Id == itemId);

            if (lastItem != null)
            {
                OnItemRemoved?.Invoke(lastItem);
            }
        }

        [Button]
        public void RemoveItems(InventoryItemConfig itemConfig, int amount)
        {
            var itemId = itemConfig.PrototypeItem.Id;
            var lastItem = _items.LastOrDefault(i => i.Id == itemId);

            if (lastItem == null)
                return;

            var hasAmount = GetTotalItemCount(lastItem.Id);

            if (hasAmount >= amount)
            {
                OnItemsRemoved?.Invoke(lastItem, amount);
            }
        }

        [Button]
        public void ConsumeItem(InventoryItemConfig itemConfig)
        {
            var prototypeItem = itemConfig.PrototypeItem.Clone();
            var isConsumable = (prototypeItem.Flags & InventoryItemFlags.Consumable) == InventoryItemFlags.Consumable;

            if (!isConsumable)
            {
                return;
            }

            if (!HasItem(itemConfig.PrototypeItem))
            {
                return;
            }

            RemoveItem(itemConfig);
            OnItemConsumed?.Invoke(itemConfig.PrototypeItem.Clone());
            Debug.Log($"Consume Item = {itemConfig.PrototypeItem.Id}");
        }

        [Button]
        public void ConsumeItem(InventoryItem item)
        {
            var isConsumable = (item.Flags & InventoryItemFlags.Consumable) == InventoryItemFlags.Consumable;

            if (!isConsumable)
            {
                return;
            }

            if (!HasItem(item))
            {
                return;
            }

            RemoveItemSlot(item);
            OnItemConsumed?.Invoke(item);
            Debug.Log($"Consume Item = {item}");
        }

        [Button]
        public void EquipItem(InventoryItem item)
        {
            var isEquipable = (item.Flags & InventoryItemFlags.Equipable) == InventoryItemFlags.Equipable;

            if (!isEquipable)
            {
                return;
            }

            if (!HasItem(item))
            {
                return;
            }

            RemoveItemSlot(item);
            OnItemEquipped?.Invoke(item);
        }

        public int GetTotalItemCount(string itemId)
        {
            var sum = 0;

            foreach (var i in _items)
            {
                if (i.Id == itemId && i.TryGetComponent(out StackableItemComponent s))
                    sum += i.GetComponent<StackableItemComponent>().Value;
                else if (i.Id == itemId && !i.TryGetComponent(out StackableItemComponent stack))
                {
                    sum++;
                }
            }

            return sum;
        }

        public int GetStacksOfItem(string itemId)
        {
            var totalValue = 0;
            var stackSize = -1;
            var nonStackableCount = 0;

            foreach (var item in _items)
            {
                if (item.Id != itemId)
                    continue;

                if (item.TryGetComponent(out StackableItemComponent stackable))
                {
                    if (stackSize == -1)
                        stackSize = stackable.StackSize;

                    totalValue += stackable.Value;
                }
                else
                {
                    nonStackableCount++;
                }
            }

            if (stackSize > 0)
            {
                return Mathf.CeilToInt((float)totalValue / stackSize);
            }

            return nonStackableCount;
        }


        public bool HasItem(InventoryItem inventoryItem)
        {
            return _items.Any(item => item.Id == inventoryItem.Id);
        }

        public void FireItemsChangedEvent()
        {
            OnInventoryListChanged?.Invoke();
        }


        private int GetMaxAddableAmount(InventoryItem item, int amount)
        {
            var freeSlots = _slotsLimit - _items.Count;
            var freeSpace = _weightLimit - _currentWeight;

            if (!item.Flags.HasFlag(InventoryItemFlags.Stackable))
            {
                var maxAmountBySlots = freeSlots;
                var maxAmountByWeightt = Mathf.FloorToInt(freeSpace / item.Weight);
                return Mathf.Min(amount, Mathf.Min(maxAmountBySlots, maxAmountByWeightt));
            }

            var itemStack = item.GetComponent<StackableItemComponent>();
            var stackSize = itemStack.StackSize;
            var itemWeight = item.Weight;

            var maxAmountByWeight = Mathf.FloorToInt(freeSpace / itemWeight);
            var remainingToAdd = Mathf.Min(amount, maxAmountByWeight);

            var totalAddable = 0;

            // Пополняем существующие неполные стеки
            foreach (var inventoryItem in _items)
            {
                if (inventoryItem.Id == item.Id &&
                    inventoryItem.TryGetComponent(out StackableItemComponent existingStack) &&
                    !existingStack.IsFull)
                {
                    var canAdd = existingStack.StackSize - existingStack.Value;
                    var willAdd = Mathf.Min(canAdd, remainingToAdd);

                    totalAddable += willAdd;
                    remainingToAdd -= willAdd;

                    if (remainingToAdd <= 0)
                        return totalAddable;
                }
            }

            // Добавим новые стаки
            var maxNewStacksByWeight = Mathf.FloorToInt(freeSpace / (itemWeight * stackSize));
            var maxNewStacksBySlots = freeSlots;

            var possibleNewStacks = Mathf.Min(maxNewStacksByWeight, maxNewStacksBySlots);
            var possibleItemsFromNewStacks = possibleNewStacks * stackSize;

            var willAddFromNewStacks = Mathf.Min(possibleItemsFromNewStacks, remainingToAdd);

            totalAddable += willAddFromNewStacks;

            return totalAddable;
        }

        public void AddWeight(int weight)
        {
            _currentWeight += weight;
            OnWeightChanged?.Invoke(_currentWeight);
            Debug.Log($"<color=orange>Current weight: {_currentWeight}</color>");
        }

        public void DecreaseWeight(int weight)
        {
            _currentWeight -= weight;
            OnWeightChanged?.Invoke(_currentWeight);

            Debug.Log($"<color=orange>Current weight: {_currentWeight}</color>");
        }
    }
}
