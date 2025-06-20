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
                    _inventory.AddItem(itemClone);
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
                    remainingAmount -= toAdd;

                    if (remainingAmount <= 0)
                        return;
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

                _inventory.AddItem(newItemClone);
                remainingAmount -= newStackSize;
            }
        }

        public void OnItemAdded(InventoryItem newItem)
        {
            if (!newItem.Flags.HasFlag(InventoryItemFlags.Stackable) ||
                !newItem.TryGetComponent(out StackableItemComponent newStack))
            {
                _inventory.AddItem(newItem);
                return;
            }

            // Пытаемся найти подходящий неполный стек
            foreach (var i in _inventory.Items)
            {
                if (i.Id == newItem.Id &&
                    i.TryGetComponent(out StackableItemComponent existingStack) &&
                    !existingStack.IsFull)
                {
                    var freeSpace = existingStack.StackSize - existingStack.Value;
                    var toAdd = Mathf.Min(freeSpace, newStack.Value);

                    existingStack.AddValue(toAdd);
                    newStack.DecreaseValue(toAdd);

                    if (newStack.Value <= 0)
                    {
                        return; // всё ушло в существующий стек, не добавляем
                    }
                }
            }

            // Остаток — создаём новый стек
            _inventory.AddItem(newItem);
        }

        public void OnItemRemoved(InventoryItem item)
        {
            if (!item.Flags.HasFlag(InventoryItemFlags.Stackable) ||
                !item.TryGetComponent(out StackableItemComponent stackableComponent))
            {
                _inventory.RemoveItem(item);
                return;
            }

            // Уменьшаем количество
            stackableComponent.DecreaseValue(1);

            // Если количество <= 0, то удаляем только если item ещё есть в инвентаре
            if (stackableComponent.Value <= 0 && _inventory.Items.Contains(item))
            {
                _inventory.RemoveItem(item);
            }
        }

        public void OnItemsRemoved(InventoryItem item, int amountToRemove)
        {
            if (!item.Flags.HasFlag(InventoryItemFlags.Stackable))
            {
                var itemsToRemove = _inventory.Items
                    .Where(i => i.Id == item.Id)
                    .Take(amountToRemove)
                    .ToList();

                foreach (var i in itemsToRemove)
                {
                    _inventory.RemoveItem(i);
                }

                return;
            }

            // Для стакаемых — идем по всем подходящим стекам
            var stackItems = _inventory.Items
                .Where(i => i.Id == item.Id && i.TryGetComponent(out StackableItemComponent s) && s.Value > 0)
                .Reverse()
                .ToList();

            var remainingRemoveValue = amountToRemove;

            foreach (var stackItem in stackItems)
            {
                if (!stackItem.TryGetComponent(out StackableItemComponent stack))
                    continue;

                if (remainingRemoveValue <= 0)
                    break;

                var removeValue = Mathf.Min(remainingRemoveValue, stack.Value);
                stack.DecreaseValue(removeValue);
                remainingRemoveValue -= removeValue;

                if (stack.Value <= 0)
                {
                    _inventory.RemoveItem(stackItem);
                }
            }

            if (remainingRemoveValue > 0)
            {
                Debug.LogWarning(
                    $"Недостаточно предметов {item.Id} для удаления {amountToRemove}. Осталось удалить: {remainingRemoveValue}");
            }
        }

        public void Dispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
            _inventory.OnItemsRemoved -= OnItemsRemoved;
        }
    }
}
