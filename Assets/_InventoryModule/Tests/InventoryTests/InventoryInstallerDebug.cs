using System;
using System.Collections.Generic;

namespace InventoryPractice
{
    public sealed class InventoryInstallerDebug
    {
        private readonly int _slotsLimit = 16;
        private readonly int _weightLimit = 100;
        private readonly bool _useStackableInventory = true;

        private Inventory _inventory;
        private Equipment _equipment;


        private HealthInventoryObserver _healthInventoryObserver;
        private InventoryItemConsumeObserver _inventoryItemConsumeObserver;
        private StackableInventoryObserver _inventoryStackableObserver;
        private EquipableItemObserver _equipableItemObserver;

        private IInventoryStackTypeObserver _inventoryStackTypeObserver;
        private PlayerStats _playerStats;

        private readonly List<IDisposable> _disposables = new();


        public Inventory Inventory => _inventory;

        public void Initialize(Inventory inventory)
        {
            _inventory = inventory;

            _inventory.Init(_slotsLimit, _weightLimit);

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
