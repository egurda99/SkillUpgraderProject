using System;
using System.Collections.Generic;

namespace InventoryPractice
{
    public sealed class InventoryInstallerForEquipmentDebug
    {
        private readonly int _slotsLimit = 16;
        private readonly int _weightLimit = 100;
        private readonly bool _useStackableInventory = true;

        private Inventory _inventory;
        private Equipment _equipment;


        private HealthInventoryObserver _healthInventoryObserver;
        private InventoryItemConsumeObserver _inventoryItemConsumeObserver;
        private StackableInventoryObserver _inventoryStackableObserver;
        private EquipableItemObserverDebug _equipableItemObserver;

        private IInventoryStackTypeObserver _inventoryStackTypeObserver;
        private PlayerStatsDebug _playerStats;

        private readonly List<IDisposable> _disposables = new();


        public Inventory Inventory => _inventory;

        public void Initialize(Inventory inventory, PlayerStatsDebug playerStats, Equipment equipment)
        {
            _inventory = inventory;
            _equipment = equipment;
            _inventory.Init(_slotsLimit, _weightLimit);
            _playerStats = playerStats;

            if (_useStackableInventory)
            {
                _disposables.Add(_inventoryStackTypeObserver = new StackableInventoryObserver(_inventory));
            }

            else
            {
                _disposables.Add(_inventoryStackTypeObserver = new NonStackableInventoryObserver(_inventory));
            }

            _disposables.Add(_healthInventoryObserver = new HealthInventoryObserver(_inventory));
            _disposables.Add(_inventoryItemConsumeObserver = new InventoryItemConsumeObserver(_inventory));
            _disposables.Add(_equipableItemObserver =
                new EquipableItemObserverDebug(_inventory, _equipment, _playerStats));
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
