using InventoryPractice;
using MyCodeBase.UI;

namespace _InventoryPractice
{
    public interface IEquipmentView
    {
        void Show();
        void Hide();
        IEquipmentSlotView GetSlotView(EquipType type, int index);
        void InitDotween(DoTweenAnimationManager doTweenAnimationManager);
    }
}
