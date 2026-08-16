using _UpgradePractice.Scripts;
using MyCodeBase;
using UnityEngine;
using Zenject;

namespace Game.Hints
{
    public sealed class UpgradeHintInspector : HintInspector
    {
        [SerializeField] private string _upgradeId;
        [SerializeField] private BuyButton _buyButton;
        [SerializeField] private GameObject _cursor;

        private UpgradesManager _upgradesManager;
        private _UpgradePractice.Scripts.MoneyStorage _moneyStorage;

        [Inject]
        public void Construct(UpgradesManager upgradesManager, _UpgradePractice.Scripts.MoneyStorage moneyStorage)
        {
            _upgradesManager = upgradesManager;
            _moneyStorage = moneyStorage;
        }

        protected override void OnStartInspect()
        {
            _cursor.SetActive(false);

            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
            _buyButton.AddListener(OnBuyButtonClicked);

            RefreshAvailability();
        }

        protected override void OnFinishInspect()
        {
            _cursor.SetActive(false);

            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
            _buyButton.RemoveListener(OnBuyButtonClicked);
        }

        private void OnMoneyChanged(int money)
        {
            RefreshAvailability();
        }

        private void OnBuyButtonClicked()
        {
            CompleteHint();
        }

        private void RefreshAvailability()
        {
            var isAvailable = _upgradesManager.CanLevelUp(_upgradeId);
            _cursor.SetActive(isAvailable);
        }
    }
}
