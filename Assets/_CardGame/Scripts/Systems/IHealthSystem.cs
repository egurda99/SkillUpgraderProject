using System;
using UnityEngine;

namespace _CardGame.Systems
{
    public interface IHealthSystem
    {
        void TakeDamage(float amount);
        void Heal(float amount);
        void Kill();

        event Action OnDamaged;


        GameObject OwnerGameObject { get; }
    }
}
