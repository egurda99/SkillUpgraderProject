using _CardGame.Events;
using Cysharp.Threading.Tasks;
using UI;

namespace _CardGame.EventTasks
{
    public class AttackVisualTask : BaseTask
    {
        private readonly HeroView _currentHero;
        private readonly HeroView _target;
        private readonly IEventBus _eventBus;

        public AttackVisualTask(HeroView currentHero, HeroView target, IEventBus eventBus)
        {
            _currentHero = currentHero;
            _target = target;
            _eventBus = eventBus;
        }

        public override async UniTask Run()
        {
            await _currentHero.AnimateAttack(_target);
            _eventBus.RaiseEvent(new AttackAnimationCompletedEvent(_currentHero, _target));
        }
    }
}
