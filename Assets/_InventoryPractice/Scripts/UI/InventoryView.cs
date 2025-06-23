using System.Collections.Generic;
using InventoryPractice;
using MyCodeBase;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _InventoryPractice
{
    public class InventoryView : Popup
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventorySlotView _viewPrefab;

        private readonly List<ViewHolder> _viewHolders = new();

        private Inventory _inventory;

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;

            _inventory.OnInventoryListChanged += RefreshInventory;
        }

        private void RefreshInventory()
        {
            Refresh();
        }

        private void Refresh()
        {
            Hide();
            Show();
        }

        protected override void OnShow()
        {
            base.OnShow();
            Show();
        }

        protected override void OnHide()
        {
            base.OnHide();
            Hide();
        }


        [Button]
        public void Show()
        {
            var allItems = _inventory.Items;
            for (int i = 0, count = allItems.Count; i < count; i++)
            {
                var config = allItems[i];
                ShowItem(config);
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

            _viewHolders.Clear();
        }

        private void ShowItem(InventoryItem item)
        {
            var view = Instantiate(_viewPrefab, _container);
            var presenter = new InventorySlotPresenter(item, view, _inventory);
            presenter.Start();

            _viewHolders.Add(new ViewHolder(view, presenter));
        }

        private void HideItem(ViewHolder vh)
        {
            vh.Presenter.Stop();
            Destroy(vh.View.gameObject);
        }

        public override void Dispose()
        {
            base.Dispose();
            _inventory.OnInventoryListChanged -= RefreshInventory;
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
