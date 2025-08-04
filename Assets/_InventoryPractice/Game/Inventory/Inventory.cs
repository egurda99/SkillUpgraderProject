using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventoryPractice
{
    public sealed class Inventory
    {
        [ShowInInspector] [ReadOnly] private int _slotsLimit = 20;
        [ShowInInspector] [ReadOnly] private int _weightLimit = 100;

        [ShowInInspector] [ReadOnly] private List<InventoryItem> _items = new();
        private ItemFactory _itemFactory;

        public List<InventoryItem> Items => _items;

        public int SlotsLimit => _slotsLimit;


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


        [ShowInInspector] [ReadOnly] private int _currentWeight;
        private MaxAddableCalculator _maxAddableCalculator;
        private DraggedItemHandler _draggedItemHandler;

        public int CurrentWeight => _currentWeight;

        public int UsedWeight
        {
            get
            {
                var sum = 0;
                foreach (var i in _items)
                {
                    if (i != null) sum += Mathf.RoundToInt(i.Weight);
                }

                return sum;
            }
        }

        public void Init(int slotsLimit, int weightLimit)
        {
            _slotsLimit = slotsLimit;
            _weightLimit = weightLimit;

            _itemFactory = new ItemFactory();
            _maxAddableCalculator = new MaxAddableCalculator(this);
            _draggedItemHandler = new DraggedItemHandler(this);

            var nullableItem = _itemFactory.CreateNullableItem();


            for (var i = 0; i < _slotsLimit; i++)
                _items.Add(nullableItem);
        }

        public InventoryItem CreateNullableItem()
        {
            return _itemFactory.CreateNullableItem();
        }

        public void AddItem(InventoryItem item)
        {
            _items.Add(item);

            OnInventoryListChanged?.Invoke();
        }

        public bool CanAddItem(InventoryItem item)
        {
            var requiredWeight = item.Weight;

            if (!item.Flags.HasFlag(InventoryItemFlags.Stackable) && HasFreeSlot)
                return UsedWeight + requiredWeight <= _weightLimit;

            foreach (var i in _items)
            {
                if (i.Id == item.Id && i.TryGetComponent(out StackableItemComponent stack) && !stack.IsFull &&
                    CanAddWeight(i.Weight))
                {
                    return true;
                }
            }

            return CanAddWeight(requiredWeight);
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

            var amountToAdd = _maxAddableCalculator.GetMaxAddableAmount(item, amount);

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

            OnItemEquipped?.Invoke(item);
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


        public bool HasItem(InventoryItem inventoryItem)
        {
            return _items.Any(item => item.Id == inventoryItem.Id);
        }

        public void FireItemsChangedEvent()
        {
            OnInventoryListChanged?.Invoke();
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

        public void RemoveNullableItem()
        {
            foreach (var item in _items)
            {
                if (item.Id == "null")
                {
                    _items.Remove(item);
                    return;
                }
            }
        }

        public void HandleDraggedItem(InventoryItem item, int slotIndex)
        {
            _draggedItemHandler.HandleDraggedItem(item, slotIndex);
        }
    }
}
