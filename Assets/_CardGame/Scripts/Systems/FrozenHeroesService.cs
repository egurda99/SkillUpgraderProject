using System.Collections.Generic;
using UI;

namespace _CardGame.Systems
{
    public class FrozenHeroesService
    {
        private readonly HashSet<HeroView> _frozen = new();

        public HashSet<HeroView> Frozen => _frozen;

        public void FreezeHero(HeroView hero)
        {
            _frozen.Add(hero);
        }

        public bool IsHeroFrozen(HeroView hero)
        {
            return _frozen.Contains(hero);
        }

        public void UnfreezeHero(HeroView hero)
        {
            _frozen.Remove(hero);
        }
    }
}