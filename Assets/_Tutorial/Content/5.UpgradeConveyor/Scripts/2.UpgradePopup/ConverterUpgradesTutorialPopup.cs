using System.Collections.Generic;
using _UpgradePractice.Scripts;
using MyCodeBase;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class ConverterUpgradesTutorialPopup : Popup
    {
        [SerializeField] private UpgradeConveyorConfig _config;

        [SerializeField] private UpgradeView _upgradeViewPrefab;
        [SerializeField] private Transform _container;

        private UpgradesManager _upgradesManager;
        private UpgradeCatalog _upgradeCatalog;

        private readonly List<ViewHolder> _viewHolders = new();

        [Inject]
        public void Construct(UpgradesManager upgradesManager)
        {
            _upgradesManager = upgradesManager;
            _upgradeCatalog = upgradesManager.UpgradeCatalog;
        }

        protected override void OnShow()
        {
            base.OnShow();
            ShowUpgrades();
        }

        protected override void OnHide()
        {
            base.OnHide();
            HideUpgrades();
        }

        private void ShowUpgrades()
        {
            var upgrades = _upgradeCatalog.GetAllUpgrades();

            ShowUpgrade(_config.UpgradeConfig);

            for (var index = 0; index < upgrades.Length; index++)
            {
                var config = upgrades[index];
                if (config.Id == _config.UpgradeConfig.Id)
                    continue;

                ShowUpgrade(config);
            }
        }

        private void ShowUpgrade(UpgradeConfig config)
        {
            var view = Instantiate(_upgradeViewPrefab, _container);
            var presenter = new UpgradePresenter(config, view, _upgradesManager);
            presenter.Start();

            _viewHolders.Add(new ViewHolder(view, presenter));
        }

        private void HideUpgrades()
        {
            foreach (var vh in _viewHolders)
            {
                vh.Presenter.Stop();
                Destroy(vh.View.gameObject);
            }

            _viewHolders.Clear();
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
