using System;

namespace _UpgradePractice.Scripts
{
    public interface IInventory
    {
        void AddItem(ResourceItem item);
        ResourceItem RemoveItem(ResourceType type);

        void DecreaseItem(ResourceType type, int amount);
        ResourceItem PeekItem(ResourceType type);
        bool IsFull { get; }

        event Action<bool> OnBackpackFilledStateChanged;
    }
}
