using System;
using System.Linq;
using InventoryPractice;
using MyCodeBase.UI;
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
        private readonly ItemDragger _itemDragger;
        private readonly DoTweenAnimationManager _doTweenAnimationManager;

        public EquipmentPresenter(Equipment equipment, IEquipmentView view,
            InventoryItemDetailPresenter detailPresenter, ItemDragger itemDragger,
            DoTweenAnimationManager doTweenAnimationManager)
        {
            _equipment = equipment;
            _view = view;
            _detailPresenter = detailPresenter;
            _itemDragger = itemDragger;
            _doTweenAnimationManager = doTweenAnimationManager;
        }

        public void Start()
        {
            _equipment.OnEquipItem += OnEquipItem;
            _equipment.OnEquipItemView += OnEquipItemView;
            _equipment.OnUnEquipItemView += OnUnEquipedItemView;
            _equipment.OnUnEquipItem += OnUnEquipedOutItem;
            _equipment.OnDropOutItem += OnUnEquipedOutItem;

            RefreshView();
            _view.InitDotween(_doTweenAnimationManager);

            _view.Show();
        }


        private void OnUnEquipedItemView(EquipType type, InventoryItem item, int index)
        {
            var slotView = _view.GetSlotView(type, index);
            slotView.PlayUnEquipedSound();
            slotView.SetDefaultSprite();
            slotView.RemoveAllButtonListeners();
        }


        public void Stop()
        {
            _equipment.OnEquipItem -= OnEquipItem;
            _equipment.OnUnEquipItem -= OnUnEquipedOutItem;
            _equipment.OnDropOutItem -= OnUnEquipedOutItem;
            _equipment.OnUnEquipItemView -= OnUnEquipedItemView;
            _equipment.OnEquipItemView -= OnEquipItemView;
            _view.Hide();
        }

        private void OnUnEquipedOutItem(EquipType type, InventoryItem item, int index)
        {
            var slotView = _view.GetSlotView(type, index);
            slotView.PlayUnEquipedSound();
            slotView.SetDefaultSprite();
            slotView.RemoveAllButtonListeners();
        }


        private void OnEquipItem(EquipType type, InventoryItem item)
        {
            var index = _equipment.GetEquippedItems(type).IndexOf(item);
            var slotView = _view.GetSlotView(type, index);

            slotView.SetSprite(item.MetaData.Icon);
            slotView.PlayEquipSound();
            slotView.DoWiggleEffect();

            slotView.RemoveAllButtonListeners();
            slotView.AddButtonListener(() => OnSlotClicked(item, slotView));
        }

        private void OnEquipItemView(EquipType type, InventoryItem item, int arg3)
        {
            if (item == null)
            {
                OnUnEquipedOutItem(type, item, arg3);
                return;
            }

            var index = _equipment.GetEquippedItems(type).IndexOf(item);
            var slotView = _view.GetSlotView(type, index);

            slotView.SetSprite(item.MetaData.Icon);
            slotView.PlayEquipSound();

            slotView.RemoveAllButtonListeners();
            slotView.AddButtonListener(() => OnSlotClicked(item, slotView));
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
                    slotView.InitDotween(_doTweenAnimationManager);

                    if (slotView == null)
                        continue;

                    slotView.BeginDragEvent += OnBeginDrag;
                    slotView.EndDragEvent += OnEndDrag;
                    slotView.DropEvent += OnDrop;

                    if (item != null)
                    {
                        slotView.SetSprite(item.MetaData.Icon);
                        slotView.RemoveAllButtonListeners();
                        slotView.AddButtonListener(() => OnSlotClicked(item, slotView));
                    }
                    else
                    {
                        slotView.SetDefaultSprite();
                        slotView.RemoveAllButtonListeners();
                    }
                }
            }
        }

        private void OnSlotClicked(InventoryItem item, IEquipmentSlotView slotView)
        {
            _detailPresenter.ShowEquippedSlotInfo(item);
            slotView.DoPunchScale();
        }

        private void OnBeginDrag(int index, EquipType type, PointerEventData data, IEquipmentSlotView view)
        {
            var item = _equipment.GetEquippedItems(type).ElementAtOrDefault(index);
            if (item == null)
                return;

            _itemDragger.StartDrag(item, item.MetaData.Icon, DragSourceType.Equipment, "");
            view.SetDragState();
        }

        private void OnEndDrag(PointerEventData data, IEquipmentSlotView view)
        {
            _itemDragger.EndDrag();
            view.SetNormalState();
        }

        private void OnDrop(int index, EquipType type, PointerEventData data, IEquipmentSlotView view)
        {
            if (!_itemDragger.HasItem)
                return;

            _itemDragger.EndDragAfterSuccessDropAtEquipment(index, type);
            view.DoWiggleEffect();
        }
    }
}
