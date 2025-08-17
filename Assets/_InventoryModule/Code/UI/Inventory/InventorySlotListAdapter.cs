using System.Collections.Generic;
using InventoryPractice;
using MyCodeBase.UI;
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
        private readonly ItemDragger _itemDragger;
        private readonly InventoryMainView _mainView;
        private readonly DoTweenAnimationManager _doTweenAnimationManager;

        public InventorySlotListAdapter(Transform container,
            InventorySlotView slotPrefab, InventoryItemDetailPresenter detailPresenter, Inventory inventory,
            ItemDragger itemDragger, InventoryMainView mainView, DoTweenAnimationManager doTweenAnimationManager)
        {
            _container = container;
            _slotPrefab = slotPrefab;
            _detailPresenter = detailPresenter;
            _inventory = inventory;
            _itemDragger = itemDragger;
            _mainView = mainView;
            _doTweenAnimationManager = doTweenAnimationManager;
        }

        public void ShowMainView()
        {
            _mainView.Show();
        }

        public void HideMainView()
        {
            _mainView.Hide();
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


        public void ShowItemWithEffect(int slotIndex, int secondItemIndex)
        {
            var presenter = GetPresenterByIndex(slotIndex);
            presenter.DoWiggleEffect();

            if (secondItemIndex == -1)
                return;

            var secondPresenter = GetPresenterByIndex(secondItemIndex);
            secondPresenter.DoWiggleEffect();
        }

        private void ShowItem(InventoryItem item, int index)
        {
            var view = Object.Instantiate(_slotPrefab, _container);
            var presenter = new InventorySlotPresenter(item, view, _detailPresenter, index, _itemDragger,
                _doTweenAnimationManager);
            presenter.Start();
            _viewHolders.Add(new ViewHolder(view, presenter));
        }

        private InventorySlotPresenter GetPresenterByIndex(int index)
        {
            if (index < 0 || index >= _viewHolders.Count)
                return null;

            return _viewHolders[index].Presenter;
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
