using System;
using UnityEngine;

[Serializable]
public sealed class AttackData
{
    [SerializeField] private float _damage;

    public float Damage => _damage;

    public void SetDamage(float value)
    {
        _damage = value;
    }
}