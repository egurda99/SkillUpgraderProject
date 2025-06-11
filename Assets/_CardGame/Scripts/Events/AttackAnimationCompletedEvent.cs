using UI;

namespace _CardGame.Events
{
    public class AttackAnimationCompletedEvent
    {
        public readonly HeroView CurrentHero;
        public readonly HeroView Target;

        public AttackAnimationCompletedEvent(HeroView currentHero, HeroView target)
        {
            CurrentHero = currentHero;
            Target = target;
        }
    }
}
