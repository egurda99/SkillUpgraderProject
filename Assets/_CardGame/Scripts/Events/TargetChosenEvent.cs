using UI;

namespace _CardGame.Events
{
    public sealed class TargetChosenEvent
    {
        public readonly HeroView Target;

        public TargetChosenEvent(HeroView target)
        {
            Target = target;
        }
    }
}
