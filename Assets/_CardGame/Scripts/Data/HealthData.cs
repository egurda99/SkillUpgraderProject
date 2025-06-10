using System;
using UnityEngine;

[Serializable]
public sealed class HealthData
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;


    public float MaxHealth => _maxHealth;

    public float CurrentHealth => _currentHealth;

    public bool IsDead => _currentHealth <= 0;

    public HealthData()
    {
        _currentHealth = _maxHealth;
    }

    public void SetCurrentHealth(float value)
    {
        _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    }

    public void ResetCurrentHealth()
    {
        _currentHealth = _maxHealth;
    }
}
