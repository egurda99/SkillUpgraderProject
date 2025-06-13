using UI;

namespace _CardGame.Events
{
    public class TurnEndedEvent
    {
        public readonly HeroView CurrentHero;
        public readonly HeroView Target;

        public TurnEndedEvent(HeroView currentHero, HeroView target)
        {
            CurrentHero = currentHero;
            Target = target;
        }
    }
}
