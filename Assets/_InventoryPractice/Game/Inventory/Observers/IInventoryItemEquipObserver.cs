using System;

namespace InventoryPractice
{
    public interface IInventoryItemEquipObserver : IDisposable
    {
        void OnHandleItemEquipDropped(InventoryItem item, int index);
    }
}
