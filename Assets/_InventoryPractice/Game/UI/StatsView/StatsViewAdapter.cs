using System;
using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class StatsViewAdapter : IDisposable
    {
        private readonly StatsView _statsView;
        private readonly PlayerStats _playerStats;

        public StatsViewAdapter(StatsView statsView, PlayerStats playerStats)
        {
            _statsView = statsView;
            _playerStats = playerStats;

            _playerStats.OnAguilityChanged += OnAguilityChanged;
            _playerStats.OnArmorChanged += OnArmorChanged;
            _playerStats.OnHealthChanged += OnHealthChanged;
            _playerStats.OnPowerChanged += OnPowerChanged;

            SetDefaultStats();
        }

        private void SetDefaultStats()
        {
            _statsView.SetPower(_playerStats.Power.ToString());
            _statsView.SetHealth(_playerStats.Health.ToString());
            _statsView.SetAgility(_playerStats.Aguility.ToString());
            _statsView.SetArmor(_playerStats.Armor.ToString());
        }

        private void OnPowerChanged(int obj)
        {
            _statsView.SetPower(obj.ToString());
        }

        private void OnHealthChanged(int obj)
        {
            _statsView.SetHealth(obj.ToString());
        }

        private void OnArmorChanged(int obj)
        {
            _statsView.SetArmor(obj.ToString());
        }

        private void OnAguilityChanged(int value)
        {
            _statsView.SetAgility(value.ToString());
        }

        public void Dispose()
        {
            _playerStats.OnAguilityChanged -= OnAguilityChanged;
            _playerStats.OnArmorChanged -= OnArmorChanged;
            _playerStats.OnHealthChanged -= OnHealthChanged;
            _playerStats.OnPowerChanged -= OnPowerChanged;
        }
    }
}
