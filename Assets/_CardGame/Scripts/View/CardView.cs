using System;
using UnityEngine;

namespace _CardGame.View
{
    [Serializable]
    public sealed class CardView
    {
        [SerializeField] private HealthData _healthData;
        [SerializeField] private AttackData _attackData;

        public HealthData HealthData => _healthData;

        public AttackData AttackData => _attackData;
    }
}
