using System;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : IDisposable
    {
        private GameFinisher _gameFinisher;
        private readonly HealthComponent _healthComponent;

        public CharacterDeathObserver(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
            _healthComponent.OnDead += OnCharacterDeath;
        }

        [Inject]
        public void Construct(GameFinisher gameFinisher)
        {
            _gameFinisher = gameFinisher;
        }

        void IDisposable.Dispose()
        {
            _healthComponent.OnDead -= OnCharacterDeath;
        }

        private void OnCharacterDeath() => _gameFinisher.FinishGame();
    }
}
