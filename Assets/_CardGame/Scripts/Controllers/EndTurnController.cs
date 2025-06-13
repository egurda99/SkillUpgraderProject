using System;
using _CardGame.Events;
using _CardGame.Installers;

namespace _CardGame.Controllers
{
    public sealed class EndTurnController : IDisposable
    {
        private readonly IEventBus _eventBus;

        public EndTurnController(IEventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.Subscribe<AttackAnimationCompletedEvent>(OnTurnEnded);
        }

        private void OnTurnEnded(AttackAnimationCompletedEvent @event)
        {
            var currentHero = @event.CurrentHero;

            var currentHeroAbility = currentHero.GetComponent<CardInstallerBase>().CardAbility;
            currentHeroAbility?.OnTurnEnd(currentHero, @event.Target);

            _eventBus.RaiseEvent(new TurnEndedEvent(currentHero, @event.Target));
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AttackAnimationCompletedEvent>(OnTurnEnded);
        }
    }
}
