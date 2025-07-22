using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class DebugInventory : MonoBehaviour, IInventory
    {
        [SerializeField] private int _limitAmount = 10;

        [ShowInInspector] [ReadOnly] private bool _isFull;
        [ShowInInspector] [ReadOnly] private readonly List<ResourceItem> _itemsList = new();

        bool IInventory.IsFull => _isFull;

        [Button]
        public void AddItem(ResourceItem item)
        {
            var existing = _itemsList.FirstOrDefault(i => i.Type == item.Type);
            if (existing != null)
            {
                if (existing.Amount < _limitAmount)
                {
                    existing.Amount += item.Amount;
                    if (existing.Amount >= _limitAmount)
                    {
                        _isFull = true;
                    }
                }

                else
                {
                    _isFull = true;
                }
            }
            else
            {
                _itemsList.Add(new ResourceItem
                {
                    Type = item.Type,
                    Amount = item.Amount
                });
            }
        }

        [Button]
        public ResourceItem RemoveItem(ResourceType type)
        {
            var existing = _itemsList.FirstOrDefault(i => i.Type == type);
            if (existing != null)
            {
                _itemsList.Remove(existing);
                return new ResourceItem
                {
                    Type = existing.Type,
                    Amount = existing.Amount
                };
            }

            return null;
        }


        public ResourceItem PeekItem(ResourceType type)
        {
            return _itemsList.FirstOrDefault(i => i.Type == type);
        }

        public void DecreaseItem(ResourceType type, int amount)
        {
            var item = _itemsList.FirstOrDefault(i => i.Type == type);
            if (item == null)
                return;

            item.Amount -= amount;

            if (item.Amount < _limitAmount)
            {
                _isFull = false;
            }


            if (item.Amount <= 0)
                _itemsList.Remove(item);
        }
    }
}
