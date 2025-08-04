using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class InventorySlotPresenter
    {
        private readonly InventoryItem _item;

        private readonly IInventorySlotView _view;
        private readonly InventoryItemDetailPresenter _detailPresenter;
        private string _amountText;


        public InventorySlotPresenter(InventoryItem item, IInventorySlotView view,
            InventoryItemDetailPresenter detailPresenter)
        {
            _item = item;
            _view = view;
            _detailPresenter = detailPresenter;
        }

        public void Start()
        {
            _view.SetSprite(_item.MetaData.Icon);


            if (_item.Flags.HasFlag(InventoryItemFlags.Stackable) &&
                _item.TryGetComponent(out StackableItemComponent stack))
            {
                _amountText = stack.Value.ToString();
            }

            else
            {
                _amountText = " ";
            }

            _view.SetAmount(_amountText);

            _view.AddButtonListener(OnSlotClicked);
        }

        private void OnSlotClicked()
        {
            if (_item != null)
            {
                _detailPresenter.ShowItemInfo(_item, _amountText);
            }
            else
            {
                _detailPresenter.Stop();
            }
        }


        public void Stop()
        {
            _view.RemoveButtonListener(OnSlotClicked);
        }
    }
}
