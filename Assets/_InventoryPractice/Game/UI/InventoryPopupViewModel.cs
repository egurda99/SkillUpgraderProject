using System;
using System.Collections.Generic;
using InventoryPractice;
using MyCodeBase.UI;
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

        private readonly EquipmentDragObserver _equipmentDragObserver;
        public InventoryItemDetailPresenter DetailPresenter => _detailPresenter;

        public InventoryPopupViewModel(Inventory inventory,
            Equipment equipment,
            PlayerStats playerStats,
            DetailsItemPresenterFactory detailsItemPresenterFactory,
            DoTweenAnimationManager dotweenAnimationManager,
            InventoryItemDetailView detailView,
            Transform detailContainer,
            ValueWidgetView weightView,
            EquipmentView equipmentView,
            StatsView statsView,
            Transform slotsContainer,
            InventorySlotView slotPrefab, ItemDragger itemDragger, InventoryMainView inventoryMainView)
        {
            _inventory = inventory;
            _detailPresenter = detailsItemPresenterFactory.Create();
            inventoryMainView.InitDotween(dotweenAnimationManager);

            ItemDetailAdapter =
                new InventoryDetailAdapter(detailContainer, detailView, _detailPresenter, equipmentView,
                    dotweenAnimationManager);
            EquipmentPresenter = new EquipmentPresenter(equipment, equipmentView, _detailPresenter, itemDragger,
                dotweenAnimationManager);
            WeightAdapter = new WeightWidgetAdapter(weightView, inventory);
            StatsViewAdapter = new StatsViewAdapter(statsView, playerStats);
            SlotListAdapter = new InventorySlotListAdapter(slotsContainer, slotPrefab, _detailPresenter, inventory,
                itemDragger, inventoryMainView, dotweenAnimationManager);
            SuccessDragHandler = new SuccessDragHandler(itemDragger, _inventory, equipment);
            _equipmentDragObserver = new EquipmentDragObserver(itemDragger, equipmentView);

            _inventory.OnInventoryListChanged += HandleInventoryChanged;
            _inventory.OnInventoryListChangedByDragAndDrop += HandleInventoryChanged;
        }


        public void Show()
        {
            _isActive = true;

            ItemDetailAdapter.Show();
            EquipmentPresenter.Start();
            SlotListAdapter.ShowItems(Items);
            SlotListAdapter.ShowMainView();
            WeightAdapter.UpdateWeightWidget(_inventory.CurrentWeight);
        }

        public void Hide()
        {
            _isActive = false;

            SlotListAdapter.HideItems();
            ItemDetailAdapter.Hide();
            SlotListAdapter.HideMainView();
            EquipmentPresenter.Stop();
        }


        private void HandleInventoryChanged()
        {
            if (!_isActive)
                return;

            SlotListAdapter.HideItems();
            SlotListAdapter.ShowItems(Items);
        }

        private void HandleInventoryChanged(int slotIndex, int secondIndex)
        {
            if (!_isActive)
                return;

            SlotListAdapter.HideItems();
            SlotListAdapter.ShowItems(Items);
            SlotListAdapter.ShowItemWithEffect(slotIndex, secondIndex);
        }

        public void Dispose()
        {
            _inventory.OnInventoryListChanged -= HandleInventoryChanged;
            _inventory.OnInventoryListChangedByDragAndDrop -= HandleInventoryChanged;

            WeightAdapter.Dispose();
            StatsViewAdapter.Dispose();
            SuccessDragHandler.Dispose();
            _detailPresenter.Dispose();
            _equipmentDragObserver.Dispose();
        }
    }
}
