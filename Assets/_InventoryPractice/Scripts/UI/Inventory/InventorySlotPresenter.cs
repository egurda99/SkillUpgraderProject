using InventoryPractice;
using UnityEngine;

namespace _InventoryPractice
{
    public sealed class InventorySlotPresenter
    {
        private readonly InventoryItem _item;

        private readonly IInventorySlotView _view;
        private readonly Inventory _inventory;
        private readonly InventoryItemDetailPresenter _detailPresenter;
        private string _amountText;


        public InventorySlotPresenter(InventoryItem item, IInventorySlotView view, Inventory inventory,
            InventoryItemDetailPresenter detailPresenter)
        {
            _item = item;
            _view = view;
            _inventory = inventory;
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
            Debug.Log("OnSlotClicked");
            _detailPresenter.Start(_item, _amountText);
        }


        public void Stop()
        {
            _view.RemoveButtonListener(OnSlotClicked);
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
