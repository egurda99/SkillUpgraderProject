using UI;

namespace _CardGame.EventTasks
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