using System;
using _CardGame.Adapters;
using _CardGame.Systems;
using _CardGame.View;
using UI;
using UnityEngine;

namespace _CardGame.Installers
{
    public abstract class CardInstallerBase : MonoBehaviour, IDisposable
    {
        [SerializeField] protected CardView _cardView;

        protected IHealthSystem _healthSystem;
        protected IAttackSystem _attackSystem;
        protected CardStatAdapter _cardStatAdapter;
        protected HeroView _heroView;

        public IHealthSystem HealthSystem => _healthSystem;
        public IAttackSystem AttackSystem => _attackSystem;

        public CardView CardView => _cardView;

        public abstract ICardAbility CardAbility { get; }

        protected virtual void Awake()
        {
            _heroView = GetComponent<HeroView>();
            InitSystems();
            _cardStatAdapter = new CardStatAdapter(_cardView.HealthData, _cardView.AttackData, _heroView);
        }

        private void InitSystems()
        {
            InitHealthSystem();
            InitAttackSystem();
        }

        protected virtual void InitAttackSystem()
        {
            _attackSystem = new DefaultAttackSystem(_cardView.AttackData);
        }

        protected virtual void InitHealthSystem()
        {
            _healthSystem = new DefaultHealthSystem(_cardView.HealthData, gameObject);
        }


        public virtual void Dispose()
        {
            _cardStatAdapter?.Dispose();
        }
    }
}
