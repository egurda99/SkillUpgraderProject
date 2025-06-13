using _CardGame.Systems;
using Zenject;

namespace _CardGame.Installers
{
    public sealed class FreezeMageCardInstaller : CardInstallerBase
    {
        private ICardAbility _ability;
        private FrozenHeroesService _frozenHeroService;

        public override ICardAbility CardAbility => _ability;


        [Inject]
        public void Construct(FrozenHeroesService frozenHeroesService)
        {
            _frozenHeroService = frozenHeroesService;
        }

        protected override void Awake()
        {
            base.Awake();
            _ability = new DefaultAbility();
        }

        protected override void InitAttackSystem()
        {
            _attackSystem = new FreezeAttackSystem(_cardView.AttackData, _frozenHeroService);
        }
    }
}
