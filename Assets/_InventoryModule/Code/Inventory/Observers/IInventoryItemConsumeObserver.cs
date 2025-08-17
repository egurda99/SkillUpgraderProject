using System;

namespace InventoryPractice
{
    public interface IInventoryItemConsumeObserver : IDisposable
    {
        void OnItemConsumed(InventoryItem item);
    }
}
