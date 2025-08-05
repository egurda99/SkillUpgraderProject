using System.Collections.Generic;
using InventoryPractice;
using UnityEngine;

namespace _InventoryPractice
{
    public sealed class InventorySlotListAdapter
    {
        private readonly Transform _container;
        private readonly InventorySlotView _slotPrefab;

        private readonly List<ViewHolder> _viewHolders = new();
        private readonly InventoryItemDetailPresenter _detailPresenter;
        private readonly Inventory _inventory;
        private int _maxSlotCount;

        public InventorySlotListAdapter(Transform container,
            InventorySlotView slotPrefab, InventoryItemDetailPresenter detailPresenter, Inventory inventory)
        {
            _container = container;
            _slotPrefab = slotPrefab;
            _detailPresenter = detailPresenter;
            _inventory = inventory;
        }

        public void ShowItems(IReadOnlyList<InventoryItem> items)
        {
            HideItems();
            _maxSlotCount = _inventory.SlotsLimit;


            for (var i = 0; i < _maxSlotCount; i++)
            {
                var item = _inventory.Items[i];
                ShowItem(item, i);
            }
        }

        public void HideItems()
        {
            foreach (var vh in _viewHolders)
            {
                vh.Presenter?.Stop();
                Object.Destroy(vh.View.gameObject);
            }

            _viewHolders.Clear();
        }

        private void ShowItem(InventoryItem item, int index)
        {
            var view = Object.Instantiate(_slotPrefab, _container);
            var presenter = new InventorySlotPresenter(item, view, _detailPresenter, index);
            presenter.Start();
            _viewHolders.Add(new ViewHolder(view, presenter));
        }

        private readonly struct ViewHolder
        {
            public readonly InventorySlotView View;
            public readonly InventorySlotPresenter Presenter;

            public ViewHolder(InventorySlotView view, InventorySlotPresenter presenter)
            {
                View = view;
                Presenter = presenter;
            }
        }
    }
}
