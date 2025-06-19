namespace InventoryPractice
{
    public interface IInventoryObserver
    {
        void OnItemAdded(InventoryItem item);
        void OnItemRemoved(InventoryItem item);
    }
}