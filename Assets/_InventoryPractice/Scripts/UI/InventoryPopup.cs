using System.Collections.Generic;
using InventoryPractice;
using MyCodeBase;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _InventoryPractice
{
    public class InventoryPopup : Popup
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _detailContainer;
        [SerializeField] private InventorySlotView _slotViewPrefab;
        [SerializeField] private InventoryItemDetailView _inventoryItemDetailView;


        private readonly List<ViewHolder> _viewHolders = new();

        private bool _isActive;
        private Inventory _inventory;
        private InventoryItemDetailView _detailView;
        private InventoryItemDetailPresenter _detailPresenter;

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
            _detailPresenter = new InventoryItemDetailPresenter(_inventory);

            _inventory.OnInventoryListChanged += RefreshInventory;
        }

        private void RefreshInventory()
        {
            Refresh();
        }

        private void Refresh()
        {
            if (!_isActive)
                return;

            Hide();
            Show();
        }

        protected override void OnShow()
        {
            base.OnShow();

            _isActive = true;
            Show();
        }

        protected override void OnHide()
        {
            base.OnHide();
            _isActive = false;
            Hide();
        }


        [Button]
        public void Show()
        {
            var allItems = _inventory.Items;

            _detailView = Instantiate(_inventoryItemDetailView, _detailContainer);
            _detailPresenter.SetView(_detailView);

            for (int i = 0, count = allItems.Count; i < count; i++)
            {
                var item = allItems[i];
                ShowItem(item);
            }
        }

        [Button]
        public void Hide()
        {
            for (int i = 0, count = _viewHolders.Count; i < count; i++)
            {
                var vh = _viewHolders[i];
                HideItem(vh);
            }


            _detailPresenter.Stop();

            if (_detailView != null)
            {
                Destroy(_detailView.gameObject);
            }

            _viewHolders.Clear();
        }

        private void ShowItem(InventoryItem item)
        {
            var view = Instantiate(_slotViewPrefab, _container);

            var slotPresenter = new InventorySlotPresenter(item, view, _inventory, _detailPresenter);
            slotPresenter.Start();


            _viewHolders.Add(new ViewHolder(view, slotPresenter));
        }

        private void HideItem(ViewHolder vh)
        {
            vh.SlotPresenter.Stop();
            Destroy(vh.SlotView.gameObject);
        }

        public override void Dispose()
        {
            base.Dispose();
            _inventory.OnInventoryListChanged -= RefreshInventory;
        }

        private readonly struct ViewHolder
        {
            public readonly InventorySlotView SlotView;
            public readonly InventorySlotPresenter SlotPresenter;

            public ViewHolder(InventorySlotView slotView, InventorySlotPresenter slotPresenter)
            {
                SlotView = slotView;
                SlotPresenter = slotPresenter;
            }
        }
    }
}
