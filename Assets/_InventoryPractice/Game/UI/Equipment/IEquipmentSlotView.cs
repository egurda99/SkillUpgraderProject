using System;
using InventoryPractice;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _InventoryPractice
{
    public interface IEquipmentSlotView
    {
        void SetSprite(Sprite sprite);
        void SetEquipType(EquipType equipType);
        void SetDefaultSprite();
        void AddButtonListener(UnityAction action);
        void RemoveAllButtonListeners();
        void SetIndex(int i);

        event Action<int, EquipType, PointerEventData, IEquipmentSlotView> BeginDragEvent;
        event Action<PointerEventData, IEquipmentSlotView> EndDragEvent;
        event Action<int, EquipType, PointerEventData> DropEvent;
        void SetDragState();
        void SetNormalState();
        void SetHighlightedState();
    }
}
