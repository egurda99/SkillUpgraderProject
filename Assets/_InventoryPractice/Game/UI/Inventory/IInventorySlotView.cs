using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _InventoryPractice
{
    public interface IInventorySlotView
    {
        void SetSprite(Sprite sprite);
        void SetAmount(string value);
        void AddButtonListener(UnityAction action);
        void RemoveButtonListener(UnityAction action);
        void SetDragState();
        void SetNormalState();

        event UnityAction<PointerEventData> BeginDragEvent;
        event UnityAction<PointerEventData> EndDragEvent;
        event UnityAction<PointerEventData> DropEvent;
    }
}
