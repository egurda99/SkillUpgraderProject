using System;
using UnityEngine;
using Zenject;

namespace InventoryPractice
{
    public sealed class InventoryInstaller : MonoBehaviour, IDisposable
    {
        [SerializeField] private int _slotsLimit;
        [SerializeField] private bool _useStackableInventory;

        // [SerializeField] private Inventory _inventory;
        private Inventory _inventory;


        private HealthInventoryObserver _healthInventoryObserver;
        private InventoryItemConsumeObserver _inventoryItemConsumeObserver;
        private StackableInventoryObserver _inventoryStackableObserver;

        private IInventoryStackTypeObserver _inventoryStackTypeObserver;


        public Inventory Inventory => _inventory;

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }


        private void Awake()
        {
            _inventory.Init(_slotsLimit);

            if (_useStackableInventory)
            {
                _inventoryStackTypeObserver = new StackableInventoryObserver(_inventory);
            }

            else
            {
                _inventoryStackTypeObserver = new NonStackableInventoryObserver(_inventory);
            }

            _healthInventoryObserver = new HealthInventoryObserver(_inventory);
            _inventoryItemConsumeObserver = new InventoryItemConsumeObserver(_inventory);
        }


        public void Dispose()
        {
            _inventoryStackTypeObserver.Dispose();
            _healthInventoryObserver.Dispose();
            _inventoryItemConsumeObserver.Dispose();
        }
    }
}
