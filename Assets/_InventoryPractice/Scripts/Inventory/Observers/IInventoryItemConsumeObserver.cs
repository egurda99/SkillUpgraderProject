namespace InventoryPractice
{
    public interface IInventoryItemConsumeObserver
    {
        void OnItemConsumed(InventoryItem item);
    }
}