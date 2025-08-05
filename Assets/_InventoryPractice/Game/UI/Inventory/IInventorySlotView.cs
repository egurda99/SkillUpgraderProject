using UnityEngine;
using UnityEngine.Events;

namespace _InventoryPractice
{
    public interface IInventorySlotView
    {
        void SetSprite(Sprite sprite);
        void SetAmount(string value);
        void AddButtonListener(UnityAction action);
        void RemoveButtonListener(UnityAction action);
    }
}
