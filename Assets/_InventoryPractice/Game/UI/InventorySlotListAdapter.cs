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

        public InventorySlotListAdapter(
            Transform container,
            InventorySlotView slotPrefab, InventoryItemDetailPresenter detailPresenter)
        {
            _container = container;
            _slotPrefab = slotPrefab;
            _detailPresenter = detailPresenter;
        }

        public void ShowItems(IReadOnlyList<InventoryItem> items)
        {
            HideItems();

            foreach (var item in items)
                ShowItem(item);
        }

        public void HideItems()
        {
            foreach (var vh in _viewHolders)
            {
                vh.Presenter.Stop();
                Object.Destroy(vh.View.gameObject);
            }

            _viewHolders.Clear();
        }

        private void ShowItem(InventoryItem item)
        {
            var view = Object.Instantiate(_slotPrefab, _container);
            var presenter = new InventorySlotPresenter(item, view, _detailPresenter);

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
