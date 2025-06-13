using UI;

namespace _CardGame.Events
{
    public sealed class AttackAnimationCompletedEvent
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
