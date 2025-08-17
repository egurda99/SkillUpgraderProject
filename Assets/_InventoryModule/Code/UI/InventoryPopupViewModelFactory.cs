using InventoryPractice;
using MyCodeBase.UI;
using UnityEngine;

namespace _InventoryPractice
{
    public sealed class InventoryPopupViewModelFactory
    {
        private readonly Inventory _inventory;
        private readonly Equipment _equipment;
        private readonly PlayerStats _playerStats;
        private readonly DetailsItemPresenterFactory _presenterFactory;
        private readonly DoTweenAnimationManager _dotweenAnimationManager;

        public InventoryPopupViewModelFactory(
            Inventory inventory,
            Equipment equipment,
            PlayerStats playerStats,
            DetailsItemPresenterFactory presenterFactory,
            DoTweenAnimationManager doTweenAnimationManager)
        {
            _inventory = inventory;
            _equipment = equipment;
            _playerStats = playerStats;
            _presenterFactory = presenterFactory;
            _dotweenAnimationManager = doTweenAnimationManager;
        }

        public InventoryPopupViewModel Create(
            InventoryItemDetailView detailView,
            Transform detailContainer,
            ValueWidgetView weightView,
            EquipmentView equipmentView,
            StatsView statsView, Transform slotsContainer, InventorySlotView slotPrefab, ItemDragger itemDragger,
            InventoryMainView inventoryMainView)
        {
            return new InventoryPopupViewModel(
                _inventory,
                _equipment,
                _playerStats,
                _presenterFactory,
                _dotweenAnimationManager,
                detailView,
                detailContainer,
                weightView,
                equipmentView,
                statsView,
                slotsContainer,
                slotPrefab,
                itemDragger,
                inventoryMainView
            );
        }
    }
}
