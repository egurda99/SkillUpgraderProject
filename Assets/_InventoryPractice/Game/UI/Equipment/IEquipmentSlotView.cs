using UnityEngine;
using UnityEngine.Events;

namespace _InventoryPractice
{
    public interface IEquipmentSlotView
    {
        void SetSprite(Sprite sprite);
        void SetDefaultSprite();
        void AddButtonListener(UnityAction action);
        void RemoveAllButtonListeners();
        void RemoveButtonListener(UnityAction action);
    }
}
