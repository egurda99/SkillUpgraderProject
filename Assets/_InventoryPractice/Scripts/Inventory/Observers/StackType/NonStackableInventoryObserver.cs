using System.Linq;

namespace InventoryPractice
{
    public sealed class NonStackableInventoryObserver : IInventoryStackTypeObserver
    {
        private readonly Inventory _inventory;

        public NonStackableInventoryObserver(Inventory inventory)
        {
            _inventory = inventory;

            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
            _inventory.OnItemsRemoved += OnItemsRemoved;
            _inventory.OnItemsAdded += OnItemsAdded;
        }

        public void OnItemAdded(InventoryItem newItem)
        {
            _inventory.AddItem(newItem);
        }

        public void OnItemsAdded(InventoryItem newItem, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var itemClone = newItem.Clone();
                _inventory.AddItem(itemClone);
            }
        }

        public void OnItemRemoved(InventoryItem item)
        {
            _inventory.RemoveItem(item);
        }

        public void OnItemsRemoved(InventoryItem item, int amountToRemove)
        {
            var itemsToRemove = _inventory.Items
                .Where(i => i.Id == item.Id)
                .Take(amountToRemove)
                .ToList();

            foreach (var i in itemsToRemove)
            {
                _inventory.RemoveItem(i);
            }
        }

        public void Dispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
            _inventory.OnItemsRemoved -= OnItemsRemoved;
            _inventory.OnItemsAdded -= OnItemsAdded;
        }
    }
}
