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

        [ShowInInspector] [ReadOnly] private List<InventoryItem> _items = new();

        public List<InventoryItem> Items => _items;

        public int SlotsLimit => _slotsLimit;
        public int UsedSlots => _items.Sum(i => i.SlotSize);
        public bool HasFreeSlot => _items.Count < _slotsLimit;


        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem, int> OnItemsAdded;

        public event Action<InventoryItem> OnItemRemoved;
        public event Action<InventoryItem, int> OnItemsRemoved;

        public event Action<InventoryItem> OnItemConsumed;

        public event Action OnInventoryListChanged;


        public void Init(int slotsLimit)
        {
            _slotsLimit = slotsLimit;
        }

        public void AddItem(InventoryItem item)
        {
            _items.Add(item);
            OnInventoryListChanged?.Invoke();
        }

        public bool CanAddItem(InventoryItem item)
        {
            var requiredSlots = item.SlotSize;

            if (!item.Flags.HasFlag(InventoryItemFlags.Stackable))
                return UsedSlots + requiredSlots <= _slotsLimit;

            foreach (var i in _items)
            {
                if (i.Id == item.Id &&
                    i.TryGetComponent(out StackableItemComponent stack) &&
                    !stack.IsFull)
                {
                    return true;
                }
            }

            return UsedSlots + requiredSlots <= _slotsLimit;
        }


        [Button]
        public void AddItem(InventoryItemConfig itemConfig)
        {
            var item = itemConfig.PrototypeItem.Clone();

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


        public void RemoveItem(InventoryItem item)
        {
            _items.Remove(item);
            OnInventoryListChanged?.Invoke();
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

        private int GetTotalItemCount(string itemId)
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

        public int GetTotalItemCount(InventoryItem inventoryItem)
        {
            var sum = 0;
            foreach (var i in _items)
            {
                if (i.Id == inventoryItem.Id && i.TryGetComponent(out StackableItemComponent s))
                    sum += i.GetComponent<StackableItemComponent>().Value;
            }

            return sum;
        }

        public bool HasItem(InventoryItem inventoryItem)
        {
            return _items.Any(item => item.Id == inventoryItem.Id);
        }


        private int GetMaxAddableAmount(InventoryItem item, int amount)
        {
            if (!item.Flags.HasFlag(InventoryItemFlags.Stackable))
            {
                var freeSlots = _slotsLimit - _items.Count;
                return Math.Min(amount, freeSlots);
            }

            var remainingToAdd = amount;

            foreach (var inventoryItem in _items)
            {
                if (inventoryItem.Id == item.Id &&
                    inventoryItem.TryGetComponent(out StackableItemComponent existingStack) &&
                    !existingStack.IsFull)
                {
                    var freeSpace = existingStack.StackSize - existingStack.Value;
                    remainingToAdd -= freeSpace;
                    if (remainingToAdd <= 0)
                        return amount;
                }
            }


            var stackSize = item.GetComponent<StackableItemComponent>().StackSize;
            var newStacksNeeded = Mathf.CeilToInt((float)Math.Max(remainingToAdd, 0) / stackSize);

            var freeSlotsLeft = _slotsLimit - _items.Count;

            if (newStacksNeeded <= freeSlotsLeft)
            {
                return amount;
            }

            var canAddInNewStacks = freeSlotsLeft * stackSize;
            var canAddTotal = amount - Math.Max(remainingToAdd, 0) + canAddInNewStacks;

            return Math.Max(canAddTotal, 0);
        }
    }
}
