using System;
using System.Linq;
using _InventoryPractice.Game;
using InventoryPractice;
using UnityEngine.EventSystems;

namespace _InventoryPractice
{
    public sealed class EquipmentPresenter
    {
        private readonly InventoryItem _item;

        private readonly IEquipmentView _view;
        private readonly InventoryItemDetailPresenter _detailPresenter;
        private string _amountText;
        private readonly Equipment _equipment;
        private readonly DragController _dragController;

        public EquipmentPresenter(Equipment equipment, IEquipmentView view,
            InventoryItemDetailPresenter detailPresenter, DragController dragController)
        {
            _equipment = equipment;
            _view = view;
            _detailPresenter = detailPresenter;
            _dragController = dragController;
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

                    if (slotView == null)
                        continue;

                    slotView.BeginDragEvent += OnBeginDrag;
                    slotView.EndDragEvent += OnEndDrag;
                    slotView.DropEvent += OnDrop;

                    if (item != null)
                    {
                        slotView.SetSprite(item.MetaData.Icon);
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

        private void OnBeginDrag(int index, EquipType type, PointerEventData data)
        {
            var item = _equipment.GetEquippedItems(type).ElementAtOrDefault(index);
            if (item == null)
                return;

            _dragController.StartDrag(item, item.MetaData.Icon, DragSourceType.Equipment);
        }

        private void OnEndDrag(PointerEventData data)
        {
            _dragController.EndDrag();
        }

        private void OnDrop(int index, EquipType type, PointerEventData data)
        {
            if (!_dragController.HasItem)
                return;

            var draggedItem = _dragController.DraggedItem;
            _equipment.EquipItemFromDrop(draggedItem, index, type);

            _dragController.EndDragAfterSuccessDropAtEquipment(index, type);
        }
    }
}
