using InventoryPractice;
using UnityEngine;

namespace _InventoryPractice
{
    public sealed class EquipmentView : MonoBehaviour, IEquipmentView
    {
        [SerializeField] private EquipmentSlotView _helmetSlotView;
        [SerializeField] private EquipmentSlotView _armorSlotView;
        [SerializeField] private EquipmentSlotView _handOneSlotView;
        [SerializeField] private EquipmentSlotView _handSecondSlotView;
        [SerializeField] private EquipmentSlotView _bootsSlotView;


        public IEquipmentSlotView GetSlotView(EquipType type, int index)
        {
            return type switch
            {
                EquipType.Helmet => _helmetSlotView,
                EquipType.Armor => _armorSlotView,
                EquipType.Boots => _bootsSlotView,
                EquipType.Hand => index == 0 ? _handOneSlotView : _handSecondSlotView,
                _ => null
            };
        }


        private void ClearView(EquipmentSlotView slotView)
        {
            slotView.SetDefaultSprite();
            slotView.RemoveAllButtonListeners();
        }
    }
}
