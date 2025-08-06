using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace InventoryPractice
{
    public sealed class InventoryDebug : MonoBehaviour
    {
        [SerializeField] private InventoryItemConfig _woodItemConfig;
        [SerializeField] private InventoryItemConfig _armorConfig;
        [SerializeField] private InventoryItemConfig _lightArmorConfig;
        [SerializeField] private InventoryItemConfig _weaponConfig;
        [SerializeField] private InventoryItemConfig _heavyWeaponConfig;
        [SerializeField] private InventoryItemConfig _shieldConfig;
        [SerializeField] private InventoryItemConfig _lumberConfig;

        [ShowInInspector] [ReadOnly] private Inventory _inventory;

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        private void Start()
        {
            _inventory.AddItems(_woodItemConfig, 12);
            _inventory.AddItems(_armorConfig, 2);
            _inventory.AddItems(_lightArmorConfig, 2);
            _inventory.AddItems(_weaponConfig, 2);
            _inventory.AddItems(_heavyWeaponConfig, 2);
            _inventory.AddItems(_shieldConfig, 1);
            _inventory.AddItems(_lumberConfig, 3);
        }


        [Button]
        public void AddItem(InventoryItemConfig itemConfig)
        {
            _inventory.AddItemToInventory(itemConfig);
        }

        [Button]
        public void AddItems(InventoryItemConfig itemConfig, int amount)
        {
            _inventory.AddItems(itemConfig, amount);
        }

        [Button]
        public void RemoveItem(InventoryItemConfig itemConfig)
        {
            _inventory.RemoveItem(itemConfig);
        }

        [Button]
        public void RemoveItems(InventoryItemConfig itemConfig, int amount)
        {
            _inventory.RemoveItems(itemConfig, amount);
        }

        [Button]
        public void ConsumeItem(InventoryItemConfig itemConfig)
        {
            _inventory.ConsumeItem(itemConfig);
        }

        [Button]
        public void SortInventory()
        {
            _inventory.SortInventory();
        }
    }
}
