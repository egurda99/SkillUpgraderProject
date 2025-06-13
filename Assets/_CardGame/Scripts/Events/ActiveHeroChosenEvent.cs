using UI;

namespace _CardGame.Events
{
    public class ActiveHeroChosenEvent
    {
        public readonly HeroView ActiveHeroView;

        public ActiveHeroChosenEvent(HeroView activeHeroView)
        {
            ActiveHeroView = activeHeroView;
        }
    }
}
