using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class UpgradeListAdapter : MonoBehaviour
    {
        [SerializeField] private UpgradeView _viewPrefab;

        [SerializeField] private Transform _container;
        [SerializeField] private Button _closeButton;


        private UpgradeCatalog _upgradeCatalog;

        private readonly List<ViewHolder> _viewHolders = new();

        private UpgradesManager _upgradesManager;

        [Inject]
        public void Construct(UpgradesManager upgradesManager)
        {
            _upgradesManager = upgradesManager;
            _upgradeCatalog = upgradesManager.UpgradeCatalog;
        }


        [Button]
        public void Show()
        {
            var upgrades = _upgradeCatalog.GetAllUpgrades();
            for (int i = 0, count = upgrades.Length; i < count; i++)
            {
                var config = upgrades[i];
                ShowUpgrade(config);
            }

            _closeButton.onClick.AddListener(Hide);
        }

        [Button]
        public void Hide()
        {
            for (int i = 0, count = _viewHolders.Count; i < count; i++)
            {
                var vh = _viewHolders[i];
                HideUpgrade(vh);
            }

            _closeButton.onClick.RemoveListener(Hide);
            _viewHolders.Clear();
        }

        private void ShowUpgrade(UpgradeConfig config)
        {
            var view = Instantiate(_viewPrefab, _container);
            var presenter = new UpgradePresenter(config, view, _upgradesManager);
            presenter.Start();

            _viewHolders.Add(new ViewHolder(view, presenter));
        }

        private void HideUpgrade(ViewHolder vh)
        {
            vh.Presenter.Stop();
            Destroy(vh.View.gameObject);
        }

        private readonly struct ViewHolder
        {
            public readonly UpgradeView View;
            public readonly UpgradePresenter Presenter;

            public ViewHolder(UpgradeView view, UpgradePresenter presenter)
            {
                View = view;
                Presenter = presenter;
            }
        }
    }
}
