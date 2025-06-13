using _CardGame.Systems;

namespace _CardGame.Installers
{
    public sealed class PalladineCardInstaller : CardInstallerBase
    {
        private ICardAbility _ability;

        public override ICardAbility CardAbility => _ability;


        protected override void Awake()
        {
            base.Awake();
            _ability = new DefaultAbility();
        }

        protected override void InitHealthSystem()
        {
            _healthSystem = new PalladinHealthSystem(_cardView.HealthData, gameObject);
        }
    }
}
