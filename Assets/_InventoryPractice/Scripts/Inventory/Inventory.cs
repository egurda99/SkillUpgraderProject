using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventoryPractice
{
    [Serializable]
    public sealed class Inventory
    {
        public List<InventoryItem> Items = new();

        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;

        public event Action<InventoryItem> OnItemConsumed;

        public void AddItem(InventoryItem item)
        {
            Items.Add(item);
            OnItemAdded?.Invoke(item);
        }

        [Button]
        public void AddItem(InventoryItemConfig itemConfig)
        {
            var item = itemConfig.PrototypeItem.Clone();
            AddItem(item);
        }

        public void RemoveItem(InventoryItem item)
        {
            Items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        [Button]
        public void RemoveItemConfig(InventoryItemConfig itemConfig)
        {
            var item = itemConfig.PrototypeItem.Clone();
            var lastItem = Items.LastOrDefault(inventoryItem => inventoryItem.Id == item.Id);

            if (lastItem != null)
            {
                RemoveItem(lastItem);
            }
        }

        [Button]
        public void ConsumeItem(InventoryItemConfig itemConfig)
        {
            var prototypeItem = itemConfig.PrototypeItem.Clone();
            var isConsumable = (prototypeItem.Flags & InventoryItemFlags.Consumable) == InventoryItemFlags.Consumable;
            // 0011
            //*0001
            //=0001

            if (!isConsumable)
            {
                return;
            }

            if (!HasItem(itemConfig.PrototypeItem))
            {
                return;
            }

            RemoveItemConfig(itemConfig);
            OnItemConsumed?.Invoke(itemConfig.PrototypeItem.Clone());
            Debug.Log($"Consume Item = {itemConfig.PrototypeItem.Id}");
        }

        private bool HasItem(InventoryItem inventoryItem)
        {
            return Items.Any(item => item.Id == inventoryItem.Id);
        }
    }
}