using _InventoryPractice.Game;
using InventoryPractice;
using UnityEngine;

namespace _InventoryPractice
{
    public sealed class InventoryPopupViewModelFactory
    {
        private readonly Inventory _inventory;
        private readonly Equipment _equipment;
        private readonly PlayerStats _playerStats;
        private readonly DetailsItemPresenterFactory _presenterFactory;

        public InventoryPopupViewModelFactory(
            Inventory inventory,
            Equipment equipment,
            PlayerStats playerStats,
            DetailsItemPresenterFactory presenterFactory)
        {
            _inventory = inventory;
            _equipment = equipment;
            _playerStats = playerStats;
            _presenterFactory = presenterFactory;
        }

        public InventoryPopupViewModel Create(
            InventoryItemDetailView detailView,
            Transform detailContainer,
            ValueWidgetView weightView,
            EquipmentView equipmentView,
            StatsView statsView, Transform slotsContainer, InventorySlotView slotPrefab, DragController dragController)
        {
            return new InventoryPopupViewModel(
                _inventory,
                _equipment,
                _playerStats,
                _presenterFactory,
                detailView,
                detailContainer,
                weightView,
                equipmentView,
                statsView,
                slotsContainer,
                slotPrefab,
                dragController
            );
        }
    }
}
