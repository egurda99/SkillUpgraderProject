using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryPractice
{
    public sealed class InventoryInstaller : MonoBehaviour
    {
        [SerializeField] private int _slotsLimit;
        [SerializeField] private int _weightLimit = 100;
        [SerializeField] private bool _useStackableInventory;

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

        [Inject]
        public void Construct(Inventory inventory, PlayerStats playerStats, Equipment equipment)
        {
            _inventory = inventory;
            _playerStats = playerStats;
            _equipment = equipment;
        }


        private void Awake()
        {
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
            _disposables.Add(_equipableItemObserver = new EquipableItemObserver(_inventory, _equipment, _playerStats));
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
