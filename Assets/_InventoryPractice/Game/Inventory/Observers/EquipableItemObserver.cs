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

            _inventory.OnItemEquipped += OnItemEquipped;
            _equipment.OnItemEquipDrop += OnHandleItemEquipDropped;
            _equipment.OnUnEquipItem += OnUnEquiped;
            _equipment.OnDropOutItem += OnDropOutItemFromEquipment;
            _equipment.OnEquipItem += HandleOnItemEquipped;
        }

        private void HandleOnItemEquipped(EquipType arg1, InventoryItem item)
        {
            if (item.TryGetComponent(out EquipableItemComponent component))
            {
                _inventory.RemoveItemSlot(item);
                AddValuesToPlayerStats(component);
            }
        }

        public void OnHandleItemEquipDropped(InventoryItem item, int index, EquipType slotType)
        {
            if (item.TryGetComponent(out EquipableItemComponent component))
            {
                //  _inventory.RemoveItemSlot(item);
                _equipment.Equip(item, slotType, index);
                //   AddValuesToPlayerStats(component);
            }
        }

        public void OnDropOutItemFromEquipment(EquipType equipType, InventoryItem item, int index)
        {
            DecreasePlayerStats(item.GetComponent<EquipableItemComponent>());
        }

        public void OnUnEquiped(EquipType equipType, InventoryItem item, int index)
        {
            _inventory.RemoveNullableItem();
            _inventory.AddItemSlot(item);

            if (item.TryGetComponent(out EquipableItemComponent component))
            {
                DecreasePlayerStats(component);
            }
        }

        private void DecreasePlayerStats(EquipableItemComponent component)
        {
            _playerStats.DecreaseAguilitty(component.AguilityValue);
            _playerStats.DecreaseArmor(component.ArmorValue);
            _playerStats.DecreaseHealth(component.HealthValue);
            _playerStats.DecreasePower(component.PowerValue);
        }

        public void OnItemEquipped(InventoryItem item)
        {
            if (item.TryGetComponent(out EquipableItemComponent component))
            {
                _equipment.Equip(item);
                //   AddValuesToPlayerStats(component);
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
            _inventory.OnItemEquipped -= OnItemEquipped;
            _equipment.OnUnEquipItem -= OnUnEquiped;
            _equipment.OnDropOutItem -= OnDropOutItemFromEquipment;
            _equipment.OnItemEquipDrop -= OnHandleItemEquipDropped;
            _equipment.OnEquipItem -= HandleOnItemEquipped;
        }
    }
}
