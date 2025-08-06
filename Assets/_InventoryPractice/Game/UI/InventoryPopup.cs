using MyCodeBase;
using UnityEngine;
using Zenject;

namespace _InventoryPractice
{
    public sealed class InventoryPopup : Popup
    {
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private Transform _detailContainer;

        [Header("Views")] [SerializeField] private InventorySlotView _slotViewPrefab;
        [SerializeField] private InventoryItemDetailView _inventoryItemDetailView;
        [SerializeField] private ValueWidgetView _valueWidgetView;
        [SerializeField] private EquipmentView _equipmentView;
        [SerializeField] private StatsView _statsView;
        [SerializeField] private ItemDragger _itemDragger;


        private InventoryPopupViewModel _viewModel;
        private InventoryPopupViewModelFactory _viewModelFactory;

        [Inject]
        public void Construct(InventoryPopupViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        protected override void OnShow()
        {
            base.OnShow();

            _viewModel = _viewModelFactory.Create(
                _inventoryItemDetailView,
                _detailContainer,
                _valueWidgetView,
                _equipmentView,
                _statsView,
                _slotsContainer,
                _slotViewPrefab,
                _itemDragger
            );

            _viewModel.Show();
        }

        protected override void OnHide()
        {
            base.OnHide();

            _viewModel.Hide();
            _viewModel.Dispose();
            _viewModel = null;
        }


        public override void Dispose()
        {
            base.Dispose();
            _viewModel?.Dispose();
        }
    }
}
