using TestsPractice;

namespace InventoryPractice
{
    public sealed class EquipableItemObserverDebug : IInventoryItemEquipObserver, IUnEquipedObserver,
        IEquippedItemDropObserver
    {
        private readonly Inventory _inventory;
        private readonly Equipment _equipment;
        private readonly PlayerStatsDebug _playerStats;

        public EquipableItemObserverDebug(Inventory inventory, Equipment equipment, PlayerStatsDebug playerStats)
        {
            _inventory = inventory;
            _equipment = equipment;
            _playerStats = playerStats;
            _equipment.OnUnEquipItemToConcreteSlot += OnUnEquippedToSlot;

            _inventory.OnItemEquipped += OnItemEquipEvent;
            _equipment.OnEquipItem += HandleOnItemEquipped;


            _equipment.OnUnEquipItem += OnUnEquiped;
            _equipment.OnDropOutItem += OnDropOutItemFromEquipment;
        }

        private void OnUnEquippedToSlot(InventoryItem itemFromEquipment, int slotIndex)
        {
            var inventoryItem = _inventory.ReplaceItemAt(itemFromEquipment, slotIndex);

            if (inventoryItem == null || inventoryItem.Id == "null")
            {
                _equipment.UnEquipFromDrop(itemFromEquipment);
                _inventory.AddWeight(itemFromEquipment.Weight);
                DecreasePlayerStats(itemFromEquipment.GetComponent<EquipableItemComponentDebug>());
                return;
            }

            if (!inventoryItem.TryGetComponent(out EquipableItemComponentDebug newEquipable))
                return;

            var oldEquipable = itemFromEquipment.GetComponent<EquipableItemComponentDebug>();
            if (newEquipable.EquipType != oldEquipable.EquipType)
                return;

            _inventory.ReplaceItemAt(itemFromEquipment, slotIndex, true);
            DecreasePlayerStats(oldEquipable);
            _inventory.AddWeight(itemFromEquipment.Weight);

            _equipment.UnEquipFromDrop(itemFromEquipment);
            _equipment.Equip(inventoryItem);
        }

        private void HandleOnItemEquipped(EquipType arg1, InventoryItem item)
        {
            if (item.TryGetComponent(out EquipableItemComponentDebug component))
            {
                _inventory.RemoveItemSlot(item);
                AddValuesToPlayerStats(component);
            }
        }

        public void OnItemEquipEvent(InventoryItem item)
        {
            if (item.TryGetComponent(out EquipableItemComponent component))
            {
                _equipment.Equip(item);
            }
        }

        public void OnDropItemFromEquipment(EquipType equipType, InventoryItem item, int index)
        {
            DecreasePlayerStats(item.GetComponent<EquipableItemComponentDebug>());
        }

        public void OnUnEquiped(EquipType equipType, InventoryItem item, int index)
        {
            _inventory.AddItemSlot(item);
            DecreasePlayerStats(item.GetComponent<EquipableItemComponentDebug>());
        }

        private void DecreasePlayerStats(EquipableItemComponentDebug component)
        {
            _playerStats.DecreaseAguilitty(component.AguilityValue);
            _playerStats.DecreaseArmor(component.ArmorValue);
            _playerStats.DecreaseHealth(component.HealthValue);
            _playerStats.DecreasePower(component.PowerValue);
        }

        public void OnItemEquipped(InventoryItem item)
        {
            if (item.TryGetComponent(out EquipableItemComponentDebug component))
            {
                _equipment.Equip(item);
                AddValuesToPlayerStats(component);
            }
        }

        private void AddValuesToPlayerStats(EquipableItemComponentDebug component)
        {
            _playerStats.AddAguilitty(component.AguilityValue);
            _playerStats.AddArmor(component.ArmorValue);
            _playerStats.AddHealth(component.HealthValue);
            _playerStats.AddPower(component.PowerValue);
        }

        public void Dispose()
        {
            _inventory.OnItemEquipped -= OnItemEquipped;
            _equipment.OnUnEquipItem -= OnUnEquiped;
            _equipment.OnDropOutItem -= OnDropItemFromEquipment;
        }

        public void OnDropOutItemFromEquipment(EquipType equipType, InventoryItem item, int index)
        {
            DecreasePlayerStats(item.GetComponent<EquipableItemComponentDebug>());
        }
    }
}
