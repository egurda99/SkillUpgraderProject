using System;
using System.Linq;
using _CardGame.Events;
using _CardGame.Installers;
using _CardGame.Teams;
using UI;

namespace _CardGame.Controllers
{
    public sealed class HeroesDeathCheckController : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly HeroListView _redTeam;
        private readonly HeroListView _blueTeam;

        public HeroesDeathCheckController(UIService uiService, IEventBus eventBus)
        {
            _eventBus = eventBus;

            _redTeam = uiService.GetRedPlayerList();
            _blueTeam = uiService.GetBluePlayerList();

            _eventBus.Subscribe<AttackAnimationCompletedEvent>(OnAttackAnimationEnded);
        }

        private void OnAttackAnimationEnded(AttackAnimationCompletedEvent @event)
        {
            var currentHero = @event.CurrentHero;
            var targetHero = @event.Target;

            CheckHeroIsAlive(currentHero);
            CheckHeroIsAlive(targetHero);
        }

        private void CheckHeroIsAlive(HeroView hero)
        {
            if (hero.gameObject.GetComponent<CardInstaller>().CardView.HealthData.IsDead)
            {
                var currentTeam = GetTeamForHero(hero);

                if (currentTeam == null)
                    return;


                if (currentTeam == Team.Blue)
                {
                    _blueTeam.Remove(hero);
                }

                else
                {
                    _redTeam.Remove(hero);
                }
            }
        }


        public Team? GetTeamForHero(HeroView view)
        {
            if (_redTeam.GetViews().Contains(view))
                return Team.Red;

            if (_blueTeam.GetViews().Contains(view))
                return Team.Blue;

            return null; // Не найден — возможно, уже удалён
        }


        public void Dispose()
        {
            _eventBus.Unsubscribe<AttackAnimationCompletedEvent>(OnAttackAnimationEnded);
        }
    }
}
