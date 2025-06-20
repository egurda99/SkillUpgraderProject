using System;

namespace InventoryPractice
{
    public sealed class HealthInventoryObserver : IInventoryObserver
    {
        private readonly Inventory _inventory;

        public HealthInventoryObserver(Inventory inventory)
        {
            _inventory = inventory;

            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
        }

        public void OnItemAdded(InventoryItem newItem)
        {
            var isEffectable = newItem.Flags.HasFlag(InventoryItemFlags.Effectable);

            if (!isEffectable)
            {
                return;
            }

            if (newItem.TryGetComponent(out HealthItemComponent component))
            {
                // Hero.Instance.MaxHitPoints += component.Health;
            }
        }

        public void OnItemsAdded(InventoryItem newItem, int amount)
        {
            throw new NotImplementedException();
        }

        public void OnItemRemoved(InventoryItem item)
        {
            var isEffectable = (item.Flags & InventoryItemFlags.Effectable) == InventoryItemFlags.Effectable;

            if (!isEffectable)
            {
                return;
            }

            if (item.TryGetComponent(out HealthItemComponent component))
            {
                // Hero.Instance.MaxHitPoints -= component.Health;
            }
        }

        public void OnItemsRemoved(InventoryItem item, int amountToRemove)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
        }
    }
}
