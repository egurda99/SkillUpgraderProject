using System;
using InventoryPractice;

namespace _InventoryPractice.Game
{
    public sealed class SuccessDragObserver : IDisposable
    {
        private readonly DragController _dragController;
        private readonly Inventory _inventory;
        private readonly Equipment _equipment;
        private readonly InventoryDetailAdapter _itemDetailAdapter;

        public SuccessDragObserver(DragController dragController, Inventory inventory, Equipment equipment,
            InventoryDetailAdapter itemDetailAdapter)
        {
            _dragController = dragController;
            _inventory = inventory;
            _equipment = equipment;
            _itemDetailAdapter = itemDetailAdapter;

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

            //  _itemDetailAdapter.Hide();
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
                // _equipment.EquipItemFromDragAndDrop(item, index, equipType);
                _equipment.Equip(item, equipType, index);
            }

            else if (type == DragSourceType.Inventory)
            {
                _equipment.EquipItemByDragAndDropFromInventory(item, index, equipType, slotIndex);
            }

            //  _itemDetailAdapter.Hide();
        }

        public void Dispose()
        {
            _dragController.OnSuccessDragEventAtEquipment -= HandleEquipmentDragEvent;
            _dragController.OnSuccessDragEventAtInventory -= HandleInventoryDragEvent;
        }
    }
}
