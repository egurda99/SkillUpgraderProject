using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class InventoryItemDetailPresenter
    {
        private IInventoryItemDetailView _view;
        private readonly Inventory _inventory;
        private InventoryItem _item;

        public InventoryItemDetailPresenter(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void SetView(IInventoryItemDetailView view)
        {
            _view = view;
            _view.Hide();
        }

        public void Start(InventoryItem item, string amountText)
        {
            Stop();

            _item = item;
            _view.SetIcon(_item.MetaData.Icon);
            _view.SetName(_item.MetaData.Name);
            _view.SetDescription(_item.MetaData.Description);
            _view.SetWeight($"Weight: {_item.Weight}");
            _view.SetAmount(amountText);

            _view.ShowUseButton(_item.Flags.HasFlag(InventoryItemFlags.Consumable));
            _view.ShowEquipButton(_item.Flags.HasFlag(InventoryItemFlags.Equipable));
            _view.ShowDropButton(true);

            _view.SetUseActionListener(ConsumeItem);
            _view.SetEquipActionListener(EquipItem);
            _view.SetDropActionListener(DropItem);

            _view.Show();
        }

        public void Stop()
        {
            if (_view == null)
                return;

            _view.RemoveDropActionListener(DropItem);
            _view.RemoveEquipActionListener(EquipItem);
            _view.RemoveUseActionListener(ConsumeItem);
        }

        private void DropItem()
        {
            _inventory.RemoveItemSlot(_item);
        }

        private void EquipItem()
        {
            _inventory.EquipItem(_item);
        }

        private void ConsumeItem()
        {
            _inventory.ConsumeItem(_item);
        }
    }
}
