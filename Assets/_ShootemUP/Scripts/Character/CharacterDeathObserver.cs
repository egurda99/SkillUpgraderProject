using System;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : IDisposable
    {
        private readonly GameFinisher _gameFinisher;
        private readonly HealthComponent _healthComponent;

        public CharacterDeathObserver(GameFinisher gameFinisher, HealthComponent healthComponent)
        {
            _gameFinisher = gameFinisher;
            _healthComponent = healthComponent;
            _healthComponent.OnDead += OnCharacterDeath;
        }

        void IDisposable.Dispose()
        {
            _healthComponent.OnDead -= OnCharacterDeath;
        }

        private void OnCharacterDeath() => _gameFinisher.FinishGame();
    }
}
