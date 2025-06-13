using System;
using UI;

namespace _CardGame.Adapters
{
    public class CardStatAdapter : IDisposable
    {
        private readonly HealthData _healthData;
        private readonly HeroView _heroView;
        private readonly AttackData _attackData;

        public CardStatAdapter(HealthData healthData, AttackData attackData, HeroView heroView)
        {
            _healthData = healthData;
            _heroView = heroView;
            _attackData = attackData;

            _healthData.OnHealthChanged += OnHealthChanged;
            SetStats(_healthData.CurrentHealth);
        }

        private void OnHealthChanged(float value)
        {
            SetStats(value);
        }

        private void SetStats(float value)
        {
            var stats = $"{_attackData.Damage} / {value}";
            _heroView.SetStats(stats);
        }

        public void Dispose()
        {
            _healthData.OnHealthChanged -= OnHealthChanged;
        }
    }
}
