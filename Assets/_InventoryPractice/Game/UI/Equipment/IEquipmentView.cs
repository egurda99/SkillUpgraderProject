using InventoryPractice;

namespace _InventoryPractice
{
    public interface IEquipmentView
    {
        void Show();
        void Hide();
        IEquipmentSlotView GetSlotView(EquipType type, int index);
    }
}
