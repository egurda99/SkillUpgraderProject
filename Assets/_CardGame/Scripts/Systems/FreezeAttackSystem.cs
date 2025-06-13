using UI;

namespace _CardGame.Systems
{
    public sealed class FreezeAttackSystem : IAttackSystem
    {
        private readonly AttackData _damageData;
        private readonly FrozenHeroesService _frozenHeroesService;

        public FreezeAttackSystem(AttackData damageData, FrozenHeroesService frozenHeroesService)
        {
            _damageData = damageData;
            _frozenHeroesService = frozenHeroesService;
        }

        public void DealDamage(IHealthSystem targetHealth)
        {
            targetHealth.TakeDamage(_damageData.Damage);
            var heroView = targetHealth.OwnerGameObject.GetComponent<HeroView>();
            if (heroView != null)
            {
                _frozenHeroesService.FreezeHero(heroView);
            }
        }
    }
}
