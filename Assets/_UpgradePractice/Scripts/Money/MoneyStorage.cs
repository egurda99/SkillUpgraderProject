using System;
using Sirenix.OdinInspector;

namespace _UpgradePractice.Scripts
{
    public sealed class MoneyStorage
    {
        private int _money;


        public event Action<int> OnMoneyChanged;
        public event Action<int> OnMoneyEarned;
        public event Action<int> OnMoneySpent;

        public int Money => _money;


        [Title("Methods")]
        [Button]
        [GUIColor(0, 1, 0)]
        public void EarnMoney(int amount)
        {
            if (amount == 0)
            {
                return;
            }

            if (amount < 0)
            {
                throw new Exception($"Can not earn negative money {amount}");
            }

            var previousValue = _money;
            var newValue = previousValue + amount;

            _money = newValue;
            OnMoneyChanged?.Invoke(newValue);
            OnMoneyEarned?.Invoke(amount);
        }

        [Button]
        [GUIColor(0, 1, 0)]
        public void SpendMoney(int amount)
        {
            if (amount == 0)
            {
                return;
            }

            if (amount < 0)
            {
                throw new Exception($"Can not spend negative money {amount}");
            }

            var previousValue = _money;
            var newValue = previousValue - amount;
            if (newValue < 0)
            {
                throw new Exception(
                    $"Negative money after spend. Money in bank: {previousValue}, spend amount {amount} ");
            }

            _money = newValue;
            OnMoneyChanged?.Invoke(newValue);
            OnMoneySpent?.Invoke(amount);
        }

        [Button]
        [GUIColor(0, 1, 0)]
        public void SetupMoney(int money)
        {
            _money = money;
            OnMoneyChanged?.Invoke(money);
        }

        public bool CanSpendMoney(int amount)
        {
            return _money >= amount;
        }
    }
}
