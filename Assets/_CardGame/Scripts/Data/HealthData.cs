using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public sealed class HealthData
{
    [SerializeField] private float _maxHealth;

    [ShowInInspector] [ReadOnly] private float _currentHealth;

    private bool _isDead;
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    public float MaxHealth => _maxHealth;

    public float CurrentHealth => _currentHealth;

    public bool IsDead => _isDead;

    public void SetCurrentHealth(float value)
    {
        _currentHealth = Mathf.Clamp(value, 0, _maxHealth);

        if (_currentHealth <= 0)
        {
            _isDead = true;
            OnDeath?.Invoke();
        }

        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void ResetCurrentHealth()
    {
        _currentHealth = _maxHealth;
        OnHealthChanged?.Invoke(_currentHealth);
    }
}
