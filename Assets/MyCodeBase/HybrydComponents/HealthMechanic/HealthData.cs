using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MyCodeBase.HybridComponents
{
    [Serializable]
    public sealed class HealthData
    {
        [SerializeField] private float _health;


        [SerializeField] private bool _isAlive;
        public float Health => _health;

        public bool IsAlive => _isAlive;

        public event Action<bool> OnIsAliveChanged;

        [Button]
        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                _isAlive = false;
                OnIsAliveChanged?.Invoke(_isAlive);
            }
        }

        [Button]
        public void AddHealth(float health)
        {
            _health += health;
        }

        [Button]
        public void SetIsAlive(bool isAlive)
        {
            _isAlive = isAlive;
            OnIsAliveChanged?.Invoke(_isAlive);
        }
    }
}
