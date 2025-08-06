using UnityEngine;

namespace _InventoryPractice
{
    public sealed class InventoryDetailAdapter
    {
        private readonly Transform _container;
        private readonly InventoryItemDetailView _prefab;
        private readonly InventoryItemDetailPresenter _presenter;

        private InventoryItemDetailView _view;

        public InventoryDetailAdapter(Transform container, InventoryItemDetailView prefab,
            InventoryItemDetailPresenter presenter)
        {
            _container = container;
            _prefab = prefab;
            _presenter = presenter;
        }

        public void Show()
        {
            _view = Object.Instantiate(_prefab, _container);
            _presenter.SetView(_view);
        }

        public void Hide()
        {
            _presenter.Stop();
            if (_view != null)
                Object.Destroy(_view.gameObject);
        }
    }
}
