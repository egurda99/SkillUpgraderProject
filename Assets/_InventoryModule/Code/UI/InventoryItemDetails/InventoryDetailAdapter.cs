using MyCodeBase.UI;
using UnityEngine;

namespace _InventoryPractice
{
    public sealed class InventoryDetailAdapter
    {
        private readonly Transform _container;
        private readonly InventoryItemDetailView _prefab;
        private readonly InventoryItemDetailPresenter _presenter;
        private readonly DoTweenAnimationManager _doTweenAnimationManager;

        private InventoryItemDetailView _view;

        public InventoryDetailAdapter(Transform container, InventoryItemDetailView prefab,
            InventoryItemDetailPresenter presenter, EquipmentView equipmentView,
            DoTweenAnimationManager doTweenAnimationManager)
        {
            _container = container;
            _prefab = prefab;
            _presenter = presenter;
            _doTweenAnimationManager = doTweenAnimationManager;
            _presenter.SetEquipmentView(equipmentView);
            //  _presenter.InitDotween(doTweenAnimationManager);
        }


        public void Show()
        {
            _view = Object.Instantiate(_prefab, _container);
            _presenter.Init(_view, _doTweenAnimationManager);
        }

        public void Hide()
        {
            _presenter.Stop();
            if (_view != null)
                Object.Destroy(_view.gameObject);
        }
    }
}
