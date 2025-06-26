using System;

namespace InventoryPractice
{
    public interface IInventoryItemEquipObserver : IDisposable
    {
        void OnItemEquipped(InventoryItem item);
    }
}