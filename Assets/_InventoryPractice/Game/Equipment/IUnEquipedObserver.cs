namespace InventoryPractice
{
    public interface IUnEquipedObserver
    {
        void OnUnEquiped(EquipType equipType, InventoryItem item);
    }
}
