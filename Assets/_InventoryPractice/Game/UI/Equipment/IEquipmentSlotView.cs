using InventoryPractice;
using UnityEngine;
using UnityEngine.Events;

namespace _InventoryPractice
{
    public interface IEquipmentSlotView
    {
        void SetSprite(Sprite sprite);
        void SetEquipType(EquipType equipType);
        void SetEquipment(Equipment equipment);
        void SetDefaultSprite();
        void SetItem(InventoryItem item);
        void AddButtonListener(UnityAction action);
        void RemoveAllButtonListeners();
        void SetIndex(int i);
    }
}
