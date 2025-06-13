using System;
using UnityEngine;

namespace _CardGame.Systems
{
    public sealed class PalladinHealthSystem : IHealthSystem
    {
        private readonly HealthData _healthData;

        private bool _shieldActive = true;

        private readonly GameObject _owner;
        public GameObject OwnerGameObject => _owner;
        public event Action OnDamaged;

        public PalladinHealthSystem(HealthData data, GameObject owner)
        {
            _healthData = data;
            _healthData.ResetCurrentHealth();
            _owner = owner;
        }

        public void TakeDamage(float amount)
        {
            if (_shieldActive)
            {
                _shieldActive = false;
                Debug.Log("Palladin Shield defended");
                return;
            }


            var currentHealth = _healthData.CurrentHealth;
            var newHealth = currentHealth - amount;
            _healthData.SetCurrentHealth(newHealth);
            OnDamaged?.Invoke();
        }

        public void Heal(float amount)
        {
            var currentHealth = _healthData.CurrentHealth;
            var newHealth = currentHealth + amount;
            _healthData.SetCurrentHealth(newHealth);
        }

        public void Kill()
        {
            _healthData.SetCurrentHealth(0);
        }
    }
}
