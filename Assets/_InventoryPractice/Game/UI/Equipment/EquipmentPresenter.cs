using System;
using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class EquipmentPresenter
    {
        private readonly InventoryItem _item;

        private readonly IEquipmentView _view;
        private readonly InventoryItemDetailPresenter _detailPresenter;
        private string _amountText;
        private readonly Equipment _equipment;

        public EquipmentPresenter(Equipment equipment, IEquipmentView view,
            InventoryItemDetailPresenter detailPresenter)
        {
            _equipment = equipment;
            _view = view;
            _detailPresenter = detailPresenter;
        }

        public void Start()
        {
            RefreshView();

            _equipment.OnEquipItem += OnEquipItem;
            _equipment.OnUnEquipItem += OnUnEquipedItem;
        }


        public void Stop()
        {
            _equipment.OnEquipItem -= OnEquipItem;
            _equipment.OnUnEquipItem -= OnUnEquipedItem;
        }

        private void OnUnEquipedItem(EquipType type, InventoryItem item)
        {
            var slotView = FindSlotView(type, item);
            slotView?.SetDefaultSprite();
        }

        private void OnEquipItem(EquipType type, InventoryItem item)
        {
            var index = _equipment.GetEquippedItems(type).IndexOf(item);
            var slotView = _view.GetSlotView(type, index);

            slotView.SetSprite(item.MetaData.Icon);

            slotView.RemoveAllButtonListeners();
            slotView.AddButtonListener(() => OnSlotClicked(item));
        }

        private void RefreshView()
        {
            foreach (var pair in _equipment.EquippedItems)
            {
                var type = pair.Key;
                var items = pair.Value;

                for (var i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    var slotView = _view.GetSlotView(type, i);

                    slotView.SetSprite(item.MetaData.Icon);

                    slotView.RemoveAllButtonListeners();
                    slotView.AddButtonListener(() => OnSlotClicked(item));
                }
            }

            foreach (EquipType type in Enum.GetValues(typeof(EquipType)))
            {
                var limit = _equipment.GetSlotLimit(type);
                var items = _equipment.GetEquippedItems(type);

                for (var i = items.Count; i < limit; i++)
                {
                    var slotView = _view.GetSlotView(type, i);
                    slotView.SetDefaultSprite();

                    slotView.RemoveAllButtonListeners();
                }
            }
        }

        // private void OnSlotClicked()
        // {
        //     _detailPresenter.Start(_item, " ");
        // }

        private void OnSlotClicked(InventoryItem item)
        {
            _detailPresenter.Start(item, " ");
        }


        private IEquipmentSlotView FindSlotView(EquipType type, InventoryItem item)
        {
            var items = _equipment.GetEquippedItems(type);
            for (var i = 0; i < items.Count + 1; i++)
            {
                var view = _view.GetSlotView(type, i);
                if (view != null) return view;
            }

            return null;
        }
    }
}
