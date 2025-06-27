namespace InventoryPractice
{
    public interface IEquippedItemDropObserver
    {
        void OnDropItemFromEquipment(EquipType equipType, InventoryItem item, int index);
    }
}