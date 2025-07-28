using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hp;

        public event Action OnDead;

        public bool IsAlive()
        {
            return _hp > 0;
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                OnDead?.Invoke();
            }
        }
    }
}
