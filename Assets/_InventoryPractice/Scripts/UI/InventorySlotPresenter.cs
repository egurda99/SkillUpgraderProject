using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class InventorySlotPresenter
    {
        private readonly InventoryItem _item;

        private readonly IInventorySlotView _view;
        private readonly Inventory _inventory;


        public InventorySlotPresenter(InventoryItem item, IInventorySlotView view, Inventory inventory)
        {
            _item = item;
            _view = view;
            _inventory = inventory;
        }

        public void Start()
        {
            _view.SetSprite(_item.MetaData.Icon);

            if (_item.Flags.HasFlag(InventoryItemFlags.Stackable) &&
                _item.TryGetComponent(out StackableItemComponent stack))
            {
                _view.SetAmount(stack.Value.ToString());
            }

            else
            {
                _view.SetAmount(" ");
            }

            // _view.AddButtonListener(OnBuyClicked);
        }


        public void Stop()
        {
            //  _view.RemoveButtonListener(OnBuyClicked);
        }

        // private void OnBuyClicked()
        // {
        //     if (_upgradeManager.CanLevelUp(_item.Id))
        //     {
        //         _upgradeManager.LevelUp(_item.Id);
        //     }
        // }
    }
}
