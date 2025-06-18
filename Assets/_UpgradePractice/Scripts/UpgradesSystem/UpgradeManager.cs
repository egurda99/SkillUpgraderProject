using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class UpgradesManager
    {
        public event Action<Upgrade> OnLevelUp;

        private readonly Dictionary<string, Upgrade> _upgrades = new();

        private readonly MoneyStorage _moneyStorage;
        private readonly DiContainer _container;
        private readonly UpgradeCatalog _upgradeCatalog;

        public UpgradeCatalog UpgradeCatalog => _upgradeCatalog;

        public UpgradesManager(MoneyStorage moneyStorage, DiContainer container, UpgradeCatalog upgradeCatalog)
        {
            _moneyStorage = moneyStorage;
            _container = container;
            _upgradeCatalog = upgradeCatalog;

            Setup(_upgradeCatalog.GetAllUpgrades());
        }


        public Upgrade GetUpgrade(string id)
        {
            return _upgrades[id];
        }

        public Upgrade[] GetAllUpgrades()
        {
            return _upgrades.Values.ToArray();
        }

        public bool CanLevelUp(Upgrade upgrade)
        {
            if (upgrade.IsMaxLevel) return false;

            var price = upgrade.NextPrice;
            return _moneyStorage.CanSpendMoney(price);
        }

        public void LevelUp(Upgrade upgrade)
        {
            if (!CanLevelUp(upgrade))
                throw new Exception($"Can't level up: {upgrade.Id}");

            var price = upgrade.NextPrice;
            _moneyStorage.SpendMoney(price);

            upgrade.LevelUp();
            OnLevelUp?.Invoke(upgrade);
        }

        public bool CanLevelUp(string id)
        {
            return CanLevelUp(_upgrades[id]);
        }

        public void LevelUp(string id)
        {
            LevelUp(_upgrades[id]);
        }


        private void Setup(IEnumerable<UpgradeConfig> configs)
        {
            _upgrades.Clear();

            foreach (var config in configs)
            {
                var upgrade = config.Create();
                _container.Inject(upgrade);

                _upgrades[config.Id] = upgrade;
            }
        }
    }
}
