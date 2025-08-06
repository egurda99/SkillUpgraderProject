using System;
using InventoryPractice;

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
            }

            else if (type == DragSourceType.Equipment && _inventory.HasFreeSlot)
            {
                _equipment.TryUnequipToSlot(item, slotIndex);
            }
        }

        private void HandleEquipmentDragEvent(InventoryItem item, DragSourceType type, int index, EquipType equipType,
            int slotIndex)
        {
            if (!item.TryGetComponent(out EquipableItemComponent equipableItemComponent))
            {
                return;
            }


            if (type == DragSourceType.Equipment)
            {
                _equipment.Equip(item, equipType, index);
            }

            else if (type == DragSourceType.Inventory)
            {
                _equipment.EquipItemByDragAndDropFromInventory(item, index, equipType, slotIndex);
            }
        }

        public void Dispose()
        {
            _dragController.OnSuccessDragEventAtEquipment -= HandleEquipmentDragEvent;
            _dragController.OnSuccessDragEventAtInventory -= HandleInventoryDragEvent;
        }
    }
}
