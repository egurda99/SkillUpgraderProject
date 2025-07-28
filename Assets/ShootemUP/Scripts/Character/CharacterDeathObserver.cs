using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : IDisposable
    {
        private readonly GameFinisher _gameFinisher;
        private readonly HealthComponent _healthComponent;

        public CharacterDeathObserver(HealthComponent healthComponent, GameFinisher gameFinisher)
        {
            _healthComponent = healthComponent;
            _gameFinisher = gameFinisher;
            _healthComponent.OnDead += OnCharacterDeath;
        }

        void IDisposable.Dispose()
        {
            Debug.Log("<color=red>Dispose in character death observer</color>");
            _healthComponent.OnDead -= OnCharacterDeath;
        }

        private void OnCharacterDeath() => _gameFinisher.FinishGame();
    }
}
