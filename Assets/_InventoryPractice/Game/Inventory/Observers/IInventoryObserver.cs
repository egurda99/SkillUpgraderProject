using System;

namespace InventoryPractice
{
    public interface IInventoryObserver : IDisposable
    {
        void OnItemAdded(InventoryItem newItem);

        void OnItemsAdded(InventoryItem newItem, int amount);

        void OnItemRemoved(InventoryItem item);

        void OnItemsRemoved(InventoryItem item, int amountToRemove);
    }
}
