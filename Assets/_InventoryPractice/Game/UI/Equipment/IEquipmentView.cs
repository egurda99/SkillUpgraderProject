using InventoryPractice;

namespace _InventoryPractice
{
    public interface IEquipmentView
    {
        IEquipmentSlotView GetSlotView(EquipType type, int index);
    }
}
