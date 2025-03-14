namespace ShootEmUp
{
    public sealed class CharacterDeathObserver
    {
        private readonly GameFinisher _gameFinisher;
        private readonly HealthComponent _healthComponent;

        public CharacterDeathObserver(GameFinisher gameFinisher, HealthComponent healthComponent)
        {
            _gameFinisher = gameFinisher;
            _healthComponent = healthComponent;
            _healthComponent.OnDead += OnCharacterDeath;
        }

        private void OnCharacterDeath() => _gameFinisher.FinishGame();

        ~CharacterDeathObserver() => _healthComponent.OnDead -= OnCharacterDeath;
    }
}
