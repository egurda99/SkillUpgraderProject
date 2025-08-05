using _InventoryPractice.Game;
using InventoryPractice;
using UnityEngine.EventSystems;

namespace _InventoryPractice
{
    public sealed class InventorySlotPresenter
    {
        private readonly InventoryItem _item;
        private readonly IInventorySlotView _view;
        private readonly InventoryItemDetailPresenter _detailPresenter;
        private readonly int _slotIndex;
        private readonly DragController _dragController;

        public InventorySlotPresenter(InventoryItem item,
            IInventorySlotView view,
            InventoryItemDetailPresenter detailPresenter, int index, DragController dragController)
        {
            _item = item;
            _view = view;
            _detailPresenter = detailPresenter;
            _slotIndex = index;
            _dragController = dragController;
        }

        public void Start()
        {
            _view.DropEvent += OnDrop;

            _view.SetSprite(_item?.MetaData.Icon);
            _view.SetAmount(GetAmountText(_item));


            if (_item.Id == "null")
                return;

            _view.BeginDragEvent += OnBeginDrag;
            _view.EndDragEvent += OnEndDrag;
            _view.AddButtonListener(OnSlotClicked);
        }

        public void Stop()
        {
            _view.DropEvent -= OnDrop;

            if (_item.Id == "null")
                return;

            _view.BeginDragEvent -= OnBeginDrag;
            _view.EndDragEvent -= OnEndDrag;
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

            _dragController.StartDrag(_item, _item.MetaData.Icon, DragSourceType.Inventory, GetAmountText(_item));
        }

        private void OnEndDrag(PointerEventData eventData)
        {
            _dragController.EndDrag();
        }

        private void OnDrop(PointerEventData eventData)
        {
            if (!_dragController.HasItem)
                return;

            _dragController.EndDragAfterSuccessDropAtInventory(_slotIndex);
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
