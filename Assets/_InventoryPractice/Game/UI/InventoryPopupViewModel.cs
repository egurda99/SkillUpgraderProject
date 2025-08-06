using System;
using System.Collections.Generic;
using InventoryPractice;
using UnityEngine;

namespace _InventoryPractice
{
    public sealed class InventoryPopupViewModel : IDisposable
    {
        public IReadOnlyList<InventoryItem> Items => _inventory.Items;

        public InventoryDetailAdapter ItemDetailAdapter { get; }
        public EquipmentPresenter EquipmentPresenter { get; }
        public WeightWidgetAdapter WeightAdapter { get; }
        public StatsViewAdapter StatsViewAdapter { get; }

        public InventorySlotListAdapter SlotListAdapter { get; }
        public SuccessDragHandler SuccessDragHandler { get; }

        private readonly Inventory _inventory;
        private readonly InventoryItemDetailPresenter _detailPresenter;
        private bool _isActive;

        public InventoryItemDetailPresenter DetailPresenter => _detailPresenter;

        public InventoryPopupViewModel(Inventory inventory,
            Equipment equipment,
            PlayerStats playerStats,
            DetailsItemPresenterFactory detailsItemPresenterFactory,
            InventoryItemDetailView detailView,
            Transform detailContainer,
            ValueWidgetView weightView,
            EquipmentView equipmentView,
            StatsView statsView,
            Transform slotsContainer,
            InventorySlotView slotPrefab, ItemDragger itemDragger)
        {
            _inventory = inventory;
            _detailPresenter = detailsItemPresenterFactory.Create();

            ItemDetailAdapter = new InventoryDetailAdapter(detailContainer, detailView, _detailPresenter);
            EquipmentPresenter = new EquipmentPresenter(equipment, equipmentView, _detailPresenter, itemDragger);
            WeightAdapter = new WeightWidgetAdapter(weightView, inventory);
            StatsViewAdapter = new StatsViewAdapter(statsView, playerStats);
            SlotListAdapter = new InventorySlotListAdapter(slotsContainer, slotPrefab, _detailPresenter, inventory,
                itemDragger);
            SuccessDragHandler = new SuccessDragHandler(itemDragger, _inventory, equipment);

            _inventory.OnInventoryListChanged += HandleInventoryChanged;
        }

        public void Show()
        {
            _isActive = true;

            ItemDetailAdapter.Show();
            EquipmentPresenter.Start();
            SlotListAdapter.ShowItems(Items);
            WeightAdapter.UpdateWeightWidget(_inventory.CurrentWeight);
        }

        public void Hide()
        {
            _isActive = false;

            SlotListAdapter.HideItems();
            ItemDetailAdapter.Hide();
            EquipmentPresenter.Stop();
        }


        private void HandleInventoryChanged()
        {
            if (!_isActive)
                return;

            SlotListAdapter.HideItems();
            SlotListAdapter.ShowItems(Items);
        }

        public void Dispose()
        {
            _inventory.OnInventoryListChanged -= HandleInventoryChanged;
            WeightAdapter.Dispose();
            StatsViewAdapter.Dispose();
            SuccessDragHandler.Dispose();
            _detailPresenter.Dispose();
        }
    }
}
