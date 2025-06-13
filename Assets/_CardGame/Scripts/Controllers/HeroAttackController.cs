using System;
using _CardGame.Events;
using _CardGame.EventTasks;
using _CardGame.Installers;
using _CardGame.Pipeline;
using _CardGame.Services;
using UI;

namespace _CardGame.Controllers
{
    public sealed class HeroAttackController : IDisposable
    {
        private readonly ActiveCardService _activeCardService;
        private readonly UIService _uiService;
        private readonly IEventBus _eventBus;
        private readonly VisualPipeline _visualPipeline;


        public HeroAttackController(ActiveCardService activeCardService, IEventBus eventBus,
            VisualPipeline visualPipeline)
        {
            _activeCardService = activeCardService;

            _visualPipeline = visualPipeline;
            _eventBus = eventBus;

            _eventBus.Subscribe<TargetChosenEvent>(OnTargetChosen);
        }

        private void OnTargetChosen(TargetChosenEvent @event)
        {
            var target = @event.Target;
            var currentHero = _activeCardService.ActiveHeroView;


            var currentHeroAbility = currentHero.GetComponent<CardInstallerBase>().CardAbility;

            currentHeroAbility.OnAttack(currentHero, target);
            currentHeroAbility.OnAttacked(currentHero, target);


            _visualPipeline.AddTask(new AttackVisualTask(currentHero, target, _eventBus));
        }


        public void Dispose()
        {
            _eventBus.Unsubscribe<TargetChosenEvent>(OnTargetChosen);
        }
    }
}
