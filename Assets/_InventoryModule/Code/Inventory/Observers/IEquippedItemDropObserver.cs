namespace InventoryPractice
{
    public interface IEquippedItemDropObserver
    {
        void OnDropOutItemFromEquipment(EquipType equipType, InventoryItem item, int index);
    }
}
