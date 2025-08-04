using System;

namespace InventoryPractice
{
    public interface IInventoryItemEquipObserver : IDisposable
    {
        void OnHandleItemEquipDropped(InventoryItem arg1, int arg2, EquipType slotType);
    }
}
