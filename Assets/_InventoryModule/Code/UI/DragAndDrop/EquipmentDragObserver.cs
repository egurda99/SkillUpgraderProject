using System;
using System.Collections.Generic;
using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class EquipmentDragObserver : IDisposable
    {
        private readonly ItemDragger _itemDragger;
        private readonly EquipmentView _equipmentView;
        private List<IEquipmentSlotView> _highlightedViews = new();

        public EquipmentDragObserver(ItemDragger itemDragger, EquipmentView equipmentView)
        {
            _itemDragger = itemDragger;
            _equipmentView = equipmentView;

            _itemDragger.OnDragStarted += OnDragStarted;
            _itemDragger.OnDragFinished += OnDragFinished;
        }

        private void OnDragFinished(InventoryItem item)
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

        private void OnDragStarted(InventoryItem item, DragSourceType dragSourceType)
        {
            if (!item.TryGetComponent(out EquipableItemComponent equipableItemComponent) ||
                dragSourceType == DragSourceType.Equipment || dragSourceType == DragSourceType.None)
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

        public void Dispose()
        {
            _itemDragger.OnDragStarted -= OnDragStarted;
            _itemDragger.OnDragFinished -= OnDragFinished;
        }
    }
}
