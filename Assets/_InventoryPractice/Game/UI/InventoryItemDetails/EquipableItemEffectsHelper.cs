using System.Collections.Generic;
using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class EquipableItemEffectsHelper
    {
        private List<IEquipmentSlotView> _highlightedViews = new();
        private readonly EquipmentView _equipmentView;


        public EquipableItemEffectsHelper(EquipmentView equipmentView)
        {
            _equipmentView = equipmentView;
        }

        public void ShowEffects(bool value, InventoryItem item)
        {
            if (!value)
                return;

            if (!item.TryGetComponent(out EquipableItemComponent equipableItemComponent))
                return;

            var equipType = equipableItemComponent.EquipType;

            _highlightedViews.Clear();
            var list = new List<IEquipmentSlotView>();
            foreach (var view in _equipmentView.GetSlotViews(equipType)) list.Add(view);
            _highlightedViews = list;

            for (var index = 0; index < _highlightedViews.Count; index++)
            {
                var slotView = _highlightedViews[index];
                slotView.SetHighlightedState();
            }
        }

        public void HideEquipableItemDetails()
        {
            if (_highlightedViews.Count > 0)
            {
                for (var index = 0; index < _highlightedViews.Count; index++)
                {
                    var slotView = _highlightedViews[index];
                    slotView.SetNormalState();
                }

                _highlightedViews.Clear();
            }
        }
    }
}
