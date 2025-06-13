using System;
using System.Linq;
using _CardGame.Installers;
using UI;
using UnityEngine;

namespace _CardGame.Systems
{
    public sealed class ElectroAttackMechanic : IDisposable
    {
        private readonly UIService _uiService;
        private readonly IHealthSystem _healthSystem;
        private readonly HeroView _currentHero;
        private readonly float _damage;

        public ElectroAttackMechanic(IHealthSystem healthSystem, UIService uiService, HeroView currentHero,
            float damage)
        {
            _healthSystem = healthSystem;
            _uiService = uiService;
            _currentHero = currentHero;
            _damage = damage;

            _healthSystem.OnDamaged += OnTakeDamage;
        }

        private void OnTakeDamage()
        {
            Debug.Log("<color=red>ELECTRO</color>");
            var allHeroes = _uiService.GetRedPlayerList().GetViews()
                .Concat(_uiService.GetBluePlayerList().GetViews());

            foreach (var hero in allHeroes)
            {
                if (hero == _currentHero)
                    continue;

                var installer = hero.GetComponent<CardInstallerBase>();
                if (installer == null)
                    continue;

                var targetHealth = installer.HealthSystem;
                targetHealth.TakeDamage(_damage);
            }
        }

        public void Dispose()
        {
            _healthSystem.OnDamaged -= OnTakeDamage;
        }
    }
}
