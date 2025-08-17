using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryPractice
{
    public sealed class StackableInventoryObserver : IInventoryStackTypeObserver
    {
        private readonly Inventory _inventory;

        public StackableInventoryObserver(Inventory inventory)
        {
            _inventory = inventory;

            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
            _inventory.OnItemsRemoved += OnItemsRemoved;
            _inventory.OnItemsAdded += OnItemsAdded;
        }

        public void OnItemsAdded(InventoryItem newItem, int amount)
        {
            // For non stacked items
            if (!newItem.Flags.HasFlag(InventoryItemFlags.Stackable) ||
                !newItem.TryGetComponent(out StackableItemComponent newStackPrototype))
            {
                for (var i = 0; i < amount; i++)
                {
                    var itemClone = newItem.Clone();
                    _inventory.ReplaceFirstNullable(itemClone);

                    _inventory.AddWeight(itemClone.Weight);
                }

                return;
            }

            var maxStackSize = newStackPrototype.StackSize;
            var remainingAmount = amount;

            // Refill exist stacks of items
            foreach (var existingItem in _inventory.Items)
            {
                if (existingItem.Id == newItem.Id &&
                    existingItem.TryGetComponent(out StackableItemComponent existingStack) &&
                    !existingStack.IsFull)
                {
                    var freeSpace = existingStack.StackSize - existingStack.Value;
                    var toAdd = Mathf.Min(freeSpace, remainingAmount);

                    existingStack.AddValue(toAdd);

                    var newWeight = existingItem.Weight * toAdd;
                    _inventory.AddWeight(newWeight);
                    remainingAmount -= toAdd;

                    if (remainingAmount <= 0)
                    {
                        _inventory.FireItemsChangedEvent();
                        return;
                    }
                }
            }

            // Create new stacks
            while (remainingAmount > 0)
            {
                var newStackSize = Mathf.Min(remainingAmount, maxStackSize);
                var newItemClone = newItem.Clone();

                if (newItemClone.TryGetComponent(out StackableItemComponent stackableClone))
                {
                    stackableClone.SetValue(newStackSize);
                }

                var weightToAdd = newItemClone.Weight * newStackSize;

                _inventory.ReplaceFirstNullable(newItemClone);
                _inventory.AddWeight(weightToAdd);
                remainingAmount -= newStackSize;
            }
        }

        public void OnItemAdded(InventoryItem newItem)
        {
            if (!newItem.Flags.HasFlag(InventoryItemFlags.Stackable) ||
                !newItem.TryGetComponent(out StackableItemComponent newStack))
            {
                _inventory.ReplaceFirstNullable(newItem);
                _inventory.AddWeight(newItem.Weight);
                return;
            }

            // find not full stack
            foreach (var i in _inventory.Items)
            {
                if (i.Id == newItem.Id &&
                    i.TryGetComponent(out StackableItemComponent existingStack) &&
                    !existingStack.IsFull)
                {
                    var freeSpace = existingStack.StackSize - existingStack.Value;
                    var toAdd = Mathf.Min(freeSpace, newStack.Value);

                    existingStack.AddValue(toAdd);
                    _inventory.AddWeight(newItem.Weight * toAdd);
                    newStack.DecreaseValue(toAdd);

                    if (newStack.Value <= 0)
                    {
                        _inventory.FireItemsChangedEvent();
                        return;
                    }
                }
            }

            // create new stack
            _inventory.ReplaceFirstNullable(newItem);
            _inventory.AddWeight(newItem.Weight);
        }

        public void OnItemRemoved(InventoryItem item)
        {
            if (!item.Flags.HasFlag(InventoryItemFlags.Stackable) ||
                !item.TryGetComponent(out StackableItemComponent stackableComponent))
            {
                _inventory.ReplaceItemWithNullable(item);
                _inventory.DecreaseWeight(item.Weight);
                return;
            }

            stackableComponent.DecreaseValue(1);
            _inventory.DecreaseWeight(item.Weight);

            if (stackableComponent.Value <= 0 && _inventory.Items.Contains(item))
            {
                _inventory.ReplaceItemWithNullable(item);
                return;
            }

            _inventory.FireItemsChangedEvent();
        }

        public void OnItemsRemoved(InventoryItem item, int amountToRemove)
        {
            if (!item.Flags.HasFlag(InventoryItemFlags.Stackable))
            {
                var itemsToRemove = new List<InventoryItem>();
                foreach (var inventoryItem in _inventory.Items.Where(i => i.Id == item.Id)
                             .Take(amountToRemove))
                    itemsToRemove.Add(inventoryItem);

                foreach (var i in itemsToRemove)
                {
                    _inventory.ReplaceItemWithNullable(item);
                    _inventory.DecreaseWeight(i.Weight);
                    //     _inventory.FireItemsChangedEvent();
                }

                return;
            }

            // for stack items
            var stackItems = new List<InventoryItem>();
            foreach (var inventoryItem in _inventory.Items.Where(i =>
                             i.Id == item.Id && i.TryGetComponent(out StackableItemComponent s) && s.Value > 0)
                         .Reverse())
                stackItems.Add(inventoryItem);

            var remainingRemoveValue = amountToRemove;

            foreach (var stackItem in stackItems)
            {
                if (!stackItem.TryGetComponent(out StackableItemComponent stack))
                    continue;

                if (remainingRemoveValue <= 0)
                    break;

                var removeValue = Mathf.Min(remainingRemoveValue, stack.Value);
                stack.DecreaseValue(removeValue);
                _inventory.DecreaseWeight(stackItem.Weight * removeValue);

                remainingRemoveValue -= removeValue;

                if (stack.Value <= 0)
                {
                    _inventory.ReplaceItemWithNullable(item);
                }
            }

            if (remainingRemoveValue > 0)
            {
                Debug.LogWarning(
                    $"Недостаточно предметов {item.Id} для удаления {amountToRemove}. Осталось удалить: {remainingRemoveValue}");
            }

            //  _inventory.FireItemsChangedEvent();
        }

        public void Dispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemsAdded -= OnItemsAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
            _inventory.OnItemsRemoved -= OnItemsRemoved;
        }
    }
}
