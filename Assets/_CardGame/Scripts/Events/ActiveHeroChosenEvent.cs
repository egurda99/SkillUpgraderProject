using UI;

namespace _CardGame.Events
{
    public sealed class ActiveHeroChosenEvent
    {
        public readonly HeroView ActiveHeroView;

        public ActiveHeroChosenEvent(HeroView activeHeroView)
        {
            ActiveHeroView = activeHeroView;
        }
    }
}
