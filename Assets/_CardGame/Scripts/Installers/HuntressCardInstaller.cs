namespace _CardGame.Installers
{
    public sealed class HuntressCardInstaller : CardInstallerBase
    {
        private ICardAbility _ability;

        public override ICardAbility CardAbility => _ability;


        protected override void Awake()
        {
            base.Awake();
            _ability = new HuntressAbility();
        }
    }
}
