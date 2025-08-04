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
            _equipment.OnEquipItem += OnEquipItem;
            _equipment.OnUnEquipItem += OnUnEquipedOutItem;
            _equipment.OnDropOutItem += OnUnEquipedOutItem;

            RefreshView();
        }


        public void Stop()
        {
            _equipment.OnEquipItem -= OnEquipItem;
            _equipment.OnUnEquipItem -= OnUnEquipedOutItem;
            _equipment.OnDropOutItem -= OnUnEquipedOutItem;
        }

        private void OnUnEquipedOutItem(EquipType type, InventoryItem item, int index)
        {
            var slotView = _view.GetSlotView(type, index);
            slotView.SetDefaultSprite();
            slotView.RemoveAllButtonListeners();
        }


        private void OnEquipItem(EquipType type, InventoryItem item)
        {
            var index = _equipment.GetEquippedItems(type).IndexOf(item);
            var slotView = _view.GetSlotView(type, index);

            slotView.SetSprite(item.MetaData.Icon);
            slotView.SetItem(item);

            slotView.RemoveAllButtonListeners();
            slotView.AddButtonListener(() => OnSlotClicked(item));
        }

        private void RefreshView()
        {
            foreach (EquipType type in Enum.GetValues(typeof(EquipType)))
            {
                var limit = _equipment.GetSlotLimit(type);
                var items = _equipment.GetEquippedItems(type);

                for (var i = 0; i < limit; i++)
                {
                    var item = i < items.Count ? items[i] : null;
                    var slotView = _view.GetSlotView(type, i);
                    slotView.SetEquipType(type);
                    slotView.SetIndex(i);
                    slotView.SetEquipment(_equipment);

                    if (slotView == null)
                    {
                        continue;
                    }

                    if (item != null)
                    {
                        slotView.SetSprite(item.MetaData.Icon);
                        slotView.SetItem(item);
                        slotView.RemoveAllButtonListeners();
                        slotView.AddButtonListener(() => OnSlotClicked(item));
                    }
                    else
                    {
                        slotView.SetDefaultSprite();
                        slotView.RemoveAllButtonListeners();
                    }
                }
            }
        }

        private void OnSlotClicked(InventoryItem item)
        {
            _detailPresenter.ShowEquippedSlotInfo(item);
        }
    }
}
