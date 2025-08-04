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

                //
                // var item = i < items.Count ? items[i] : null;
                // ShowItem(item, i);
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
            view.SetSlotIndex(index);
            view.SetInventory(_inventory);
            view.SetItem(item);

            // if (item != null)
            // {
            //     var presenter = new InventorySlotPresenter(item, view, _detailPresenter);
            //     presenter.Start();
            //     _viewHolders.Add(new ViewHolder(view, presenter));
            // }

            if (item != null && item.Id != "null")
            {
                var presenter = new InventorySlotPresenter(item, view, _detailPresenter);
                presenter.Start();
                _viewHolders.Add(new ViewHolder(view, presenter));
            }
            else
            {
                // Пустой слот, без презентера
                // view.SetSprite(null);
                // view.SetAmount(string.Empty);
                _viewHolders.Add(new ViewHolder(view, null));
            }
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
