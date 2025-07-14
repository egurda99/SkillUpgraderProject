using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace InventoryPractice
{
    public sealed class InventoryDebug : MonoBehaviour
    {
        private Inventory _inventory;

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
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
    }
}
