using System.Linq;
using UnityEngine;

namespace InventoryPractice
{
    public sealed class MaxAddableCalculator
    {
        private readonly Inventory _inventory;

        public MaxAddableCalculator(Inventory inventory)
        {
            _inventory = inventory;
        }

        public int GetMaxAddableAmount(InventoryItem item, int amount)
        {
            var usedSlots = _inventory.Items.Count(i => i.Id != "null"); // только зан€тые
            var freeSlots = _inventory.SlotsLimit - usedSlots;

            var freeSpace = _inventory.WeightLimit - _inventory.CurrentWeight;

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

            // ѕополн€ем существующие неполные стеки
            foreach (var inventoryItem in _inventory.Items)
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

            // ƒобавим новые стаки
            var maxNewStacksByWeight = Mathf.FloorToInt(freeSpace / (itemWeight * stackSize));
            var maxNewStacksBySlots = freeSlots;

            var possibleNewStacks = Mathf.Min(maxNewStacksByWeight, maxNewStacksBySlots);
            var possibleItemsFromNewStacks = possibleNewStacks * stackSize;

            var willAddFromNewStacks = Mathf.Min(possibleItemsFromNewStacks, remainingToAdd);

            totalAddable += willAddFromNewStacks;

            return totalAddable;
        }
    }
}