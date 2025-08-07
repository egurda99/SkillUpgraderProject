using System;
using InventoryPractice;
using MyCodeBase.UI;

namespace _InventoryPractice
{
    public sealed class InventoryItemDetailPresenter : IDisposable
    {
        private IInventoryItemDetailView _view;
        private readonly Inventory _inventory;
        private InventoryItem _item;
        private readonly Equipment _equipment;

        private EquipableItemEffectsHelper _equipableItemEffectsHelper;


        public InventoryItemDetailPresenter(Inventory inventory, Equipment equipment)
        {
            _inventory = inventory;
            _equipment = equipment;


            _inventory.OnInventoryListChanged += Hide;
            _inventory.OnInventoryListChangedByDragAndDrop += Hide;


            _equipment.OnEquipItemView += Hide;
        }

        private void Hide(int arg1, int arg2)
        {
            Stop();
        }

        private void Hide(EquipType arg1, InventoryItem arg2, int arg3)
        {
            Stop();
        }

        private void Hide()
        {
            Stop();
        }

        public void Init(IInventoryItemDetailView view, DoTweenAnimationManager tweenAnimationManager)
        {
            _view = view;

            InitDotween(tweenAnimationManager);

            _view.Hide();
        }

        public void SetEquipmentView(EquipmentView equipmentView)
        {
            _equipableItemEffectsHelper = new EquipableItemEffectsHelper(equipmentView);
        }


        public void InitDotween(DoTweenAnimationManager tweenAnimationManager)
        {
            _view.SetTweenManager(tweenAnimationManager);
        }


        public void ShowItemInfo(InventoryItem item, string amountText)
        {
            Stop();

            _item = item;
            _view.SetIcon(_item.MetaData.Icon);
            _view.SetName(_item.MetaData.Name);
            _view.SetDescription(_item.MetaData.Description);
            _view.SetWeight($"Weight: {_item.Weight}");
            _view.SetAmount(amountText);

            _view.ShowUseButton(_item.Flags.HasFlag(InventoryItemFlags.Consumable));

            var isEquipable = _item.Flags.HasFlag(InventoryItemFlags.Equipable);

            _equipableItemEffectsHelper.ShowEffects(isEquipable, _item);

            _view.ShowEquipButton(isEquipable);
            _view.ShowUnEquipButton(false);
            _view.ShowDropButton(true);

            _view.SetUseActionListener(ConsumeItem);
            _view.SetEquipActionListener(EquipItem);
            _view.SetDropActionListener(DropItem);
            _view.Show();
        }


        public void ShowEquippedSlotInfo(InventoryItem item)
        {
            Stop();

            _item = item;
            _view.SetIcon(_item.MetaData.Icon);
            _view.SetName(_item.MetaData.Name);
            _view.SetDescription(_item.MetaData.Description);
            _view.SetWeight($"Weight: {_item.Weight}");
            _view.SetAmount(" ");

            _view.ShowUnEquipButton(true);
            _view.ShowEquipButton(false);
            _view.ShowDropButton(true);
            _view.ShowUseButton(_item.Flags.HasFlag(InventoryItemFlags.Consumable));

            _view.SetUnEquipActionListener(UnEquipItem);
            _view.SetDropActionListener(DropItemFromEquipped);
            _view.Show();
        }


        public void Stop()
        {
            if (_view == null)
                return;
            _view.Hide();
            _view.RemoveDropActionListener(DropItem);
            _view.RemoveDropActionListener(DropItemFromEquipped);
            _view.RemoveEquipActionListener(EquipItem);
            _view.RemoveUseActionListener(ConsumeItem);
            _view.RemoveUnEquipActionListener(UnEquipItem);

            _equipableItemEffectsHelper.HideEquipableItemDetails();
        }

        private void DropItem()
        {
            _inventory.RemoveItemSlot(_item);
            _view.Hide();
        }

        private void DropItemFromEquipped()
        {
            _equipment.DropItemFromEquipped(_item);
            _view.Hide();
        }

        private void EquipItem()
        {
            _inventory.EquipItem(_item);
            _view.Hide();
        }

        private void UnEquipItem()
        {
            _equipment.Unequip(_item);
            _view.Hide();
        }

        private void ConsumeItem()
        {
            _inventory.ConsumeItem(_item);
        }

        public void Dispose()
        {
            _inventory.OnInventoryListChanged -= Hide;
            _inventory.OnInventoryListChangedByDragAndDrop -= Hide;

            _equipment.OnEquipItemView -= Hide;
        }
    }
}
