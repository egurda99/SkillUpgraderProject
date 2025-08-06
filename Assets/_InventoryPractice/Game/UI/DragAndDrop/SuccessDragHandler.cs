using System;
using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class SuccessDragHandler : IDisposable
    {
        private readonly ItemDragger _itemDragger;
        private readonly Inventory _inventory;
        private readonly Equipment _equipment;

        public SuccessDragHandler(ItemDragger itemDragger, Inventory inventory, Equipment equipment)
        {
            _itemDragger = itemDragger;
            _inventory = inventory;
            _equipment = equipment;

            _itemDragger.OnSuccessDragEventAtEquipment += HandleEquipmentDragEvent;
            _itemDragger.OnSuccessDragEventAtInventory += HandleInventoryDragEvent;
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
            _itemDragger.OnSuccessDragEventAtEquipment -= HandleEquipmentDragEvent;
            _itemDragger.OnSuccessDragEventAtInventory -= HandleInventoryDragEvent;
        }
    }
}
