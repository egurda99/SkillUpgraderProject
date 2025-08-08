namespace InventoryPractice
{
    public sealed class EquipableItemObserver : IInventoryItemEquipObserver, IUnEquipedObserver,
        IEquippedItemDropObserver
    {
        private readonly Inventory _inventory;
        private readonly Equipment _equipment;
        private readonly PlayerStats _playerStats;

        public EquipableItemObserver(Inventory inventory, Equipment equipment, PlayerStats playerStats)
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


        private void HandleOnItemEquipped(EquipType arg1, InventoryItem item)
        {
            if (item.TryGetComponent(out EquipableItemComponent component))
            {
                _inventory.RemoveItemSlot(item);
                AddValuesToPlayerStats(component);
            }
        }

        public void OnDropOutItemFromEquipment(EquipType equipType, InventoryItem item, int index)
        {
            DecreasePlayerStats(item.GetComponent<EquipableItemComponent>());
        }

        public void OnUnEquiped(EquipType equipType, InventoryItem item, int index)
        {
            _inventory.ReplaceFirstNullable(item);
            _inventory.AddWeight(item.Weight);


            if (item.TryGetComponent(out EquipableItemComponent component))
            {
                DecreasePlayerStats(component);
            }
        }

        private void OnUnEquippedToSlot(InventoryItem itemFromEquipment, int slotIndex, int index)
        {
            var inventoryItem = _inventory.ReplaceItemAt(itemFromEquipment, slotIndex);

            if (inventoryItem == null || inventoryItem.Id == "null")
            {
                _equipment.UnEquipFromDrop(itemFromEquipment);
                _inventory.AddWeight(itemFromEquipment.Weight);
                DecreasePlayerStats(itemFromEquipment.GetComponent<EquipableItemComponent>());
                return;
            }

            if (!inventoryItem.TryGetComponent(out EquipableItemComponent newEquipable))
                return;

            var oldEquipable = itemFromEquipment.GetComponent<EquipableItemComponent>();
            if (newEquipable.EquipType != oldEquipable.EquipType)
                return;

            _inventory.ReplaceItemAt(itemFromEquipment, slotIndex, true);
            DecreasePlayerStats(oldEquipable);
            _inventory.AddWeight(itemFromEquipment.Weight);

            _equipment.UnEquipFromDrop(itemFromEquipment);
            _equipment.Equip(inventoryItem, newEquipable.EquipType, index);
        }

        private void DecreasePlayerStats(EquipableItemComponent component)
        {
            _playerStats.DecreaseAguilitty(component.AguilityValue);
            _playerStats.DecreaseArmor(component.ArmorValue);
            _playerStats.DecreaseHealth(component.HealthValue);
            _playerStats.DecreasePower(component.PowerValue);
        }

        public void OnItemEquipEvent(InventoryItem item)
        {
            if (item.TryGetComponent(out EquipableItemComponent component))
            {
                _equipment.Equip(item);
            }
        }

        private void AddValuesToPlayerStats(EquipableItemComponent component)
        {
            _playerStats.AddAguilitty(component.AguilityValue);
            _playerStats.AddArmor(component.ArmorValue);
            _playerStats.AddHealth(component.HealthValue);
            _playerStats.AddPower(component.PowerValue);
        }

        public void Dispose()
        {
            _inventory.OnItemEquipped -= OnItemEquipEvent;
            _equipment.OnUnEquipItem -= OnUnEquiped;
            _equipment.OnDropOutItem -= OnDropOutItemFromEquipment;
            _equipment.OnEquipItem -= HandleOnItemEquipped;
            _equipment.OnUnEquipItemToConcreteSlot -= OnUnEquippedToSlot;
        }
    }
}
