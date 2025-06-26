namespace InventoryPractice
{
    public sealed class EquipableItemObserver : IInventoryItemEquipObserver, IUnEquipedObserver
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
            _equipment.OnUnEquipItem += OnUnEquiped;
        }

        public void OnUnEquiped(EquipType equipType, InventoryItem item)
        {
            _inventory.AddItemSlot(item);
            DecreasePlayerStats(item.GetComponent<EquipableItemComponent>());
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
                AddValuesToPlayerStats(component);
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
        }
    }
}
