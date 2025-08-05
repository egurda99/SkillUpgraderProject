using _InventoryPractice.Game;
using InventoryPractice;
using UnityEngine.EventSystems;

namespace _InventoryPractice
{
    public sealed class InventorySlotPresenter
    {
        private readonly InventoryItem _item;
        private readonly InventorySlotView _view;
        private readonly InventoryItemDetailPresenter _detailPresenter;
        private readonly int _slotIndex;

        public InventorySlotPresenter(InventoryItem item,
            InventorySlotView view,
            InventoryItemDetailPresenter detailPresenter, int index)
        {
            _item = item;
            _view = view;
            _detailPresenter = detailPresenter;
            _slotIndex = index;
        }

        public void Start()
        {
            _view.BeginDragEvent += OnBeginDrag;
            _view.EndDragEvent += OnEndDrag;
            _view.DropEvent += OnDrop;

            _view.SetSprite(_item?.MetaData.Icon);
            _view.SetAmount(GetAmountText(_item));


            if (_item.Id == "null")
                return;

            _view.AddButtonListener(OnSlotClicked);
        }

        public void Stop()
        {
            _view.BeginDragEvent -= OnBeginDrag;
            _view.EndDragEvent -= OnEndDrag;
            _view.DropEvent -= OnDrop;

            if (_item.Id == "null")
                return;
            _view.RemoveButtonListener(OnSlotClicked);
        }

        private void OnSlotClicked()
        {
            if (_item == null)
                return;

            _detailPresenter.ShowItemInfo(_item, GetAmountText(_item));
        }

        private void OnBeginDrag(PointerEventData eventData)
        {
            if (_item == null)
                return;

            DragController.Instance.StartDrag(_item, _item.MetaData.Icon, DragSourceType.Inventory);
        }

        private void OnEndDrag(PointerEventData eventData)
        {
            DragController.Instance.EndDrag();
        }

        private void OnDrop(PointerEventData eventData)
        {
            if (!DragController.Instance.HasItem)
                return;

            DragController.Instance.EndDragAfterSuccessDropAtInventory(_slotIndex);
        }

        private string GetAmountText(InventoryItem item)
        {
            if (_item.Flags.HasFlag(InventoryItemFlags.Stackable) &&
                _item.TryGetComponent(out StackableItemComponent stack))
            {
                return stack.Value.ToString();
            }

            return " ";
        }
    }
}
