using System;
using InventoryPractice;
using UnityEngine;

namespace _InventoryPractice.Game
{
    public sealed class SuccessDragObserver : IDisposable
    {
        private readonly DragController _dragController;
        private readonly Inventory _inventory;
        private readonly Equipment _equipment;

        public SuccessDragObserver(DragController dragController, Inventory inventory, Equipment equipment)
        {
            _dragController = dragController;
            _inventory = inventory;
            _equipment = equipment;

            _dragController.OnSuccessDragEventAtEquipment += HandleEquipmentDragEvent;
            _dragController.OnSuccessDragEventAtInventory += HandleInventoryDragEvent;
        }

        private void HandleInventoryDragEvent(InventoryItem item, DragSourceType type, int slotIndex)
        {
            if (type == DragSourceType.Inventory)
            {
                _inventory.HandleDraggedItem(item, slotIndex);
                Debug.Log($"<color=red>HandleDraggedItem : {item} , {slotIndex}</color>");
            }

            else if (type == DragSourceType.Equipment)
            {
                _equipment.Unequip(item);
            }
        }

        private void HandleEquipmentDragEvent(InventoryItem item, DragSourceType type, int index, EquipType equipType)
        {
            if (type == DragSourceType.Equipment)
            {
                _equipment.EquipItemFromDrop(item, index, equipType);
                Debug.Log($"<color=red>HandleDraggedItem : {item} , {equipType}</color>");
            }
        }

        public void Dispose()
        {
            _dragController.OnSuccessDragEventAtEquipment -= HandleEquipmentDragEvent;
            _dragController.OnSuccessDragEventAtInventory -= HandleInventoryDragEvent;
        }
    }
}
