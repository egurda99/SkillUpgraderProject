using InventoryPractice;
using UnityEngine;
using UnityEngine.Events;

namespace _InventoryPractice
{
    public interface IInventorySlotView
    {
        void SetItem(InventoryItem item);
        void SetInventory(Inventory inventory);
        void SetSlotIndex(int index);
        void SetSprite(Sprite sprite);
        void SetAmount(string value);
        void AddButtonListener(UnityAction action);
        void RemoveButtonListener(UnityAction action);
    }
}
