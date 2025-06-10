using UI;

namespace _CardGame.EventTasks
{
    public class TargetChosenEvent
    {
        public readonly HeroView Target;

        public TargetChosenEvent(HeroView target)
        {
            Target = target;
        }
    }
}
