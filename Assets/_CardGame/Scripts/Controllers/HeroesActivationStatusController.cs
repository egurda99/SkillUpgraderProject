using System;
using _CardGame.EventTasks;
using _CardGame.Services;
using UI;

namespace _CardGame.Controllers
{
    public class HeroesActivationStatusController : IDisposable
    {
        private HeroListView _redHeroListView;
        private HeroListView _blueHeroListView;
        private ActiveCardService _activeCardService;
        private readonly UIService _uiService;
        private readonly IEventBus _eventBus;


        public HeroesActivationStatusController(UIService uiService, IEventBus eventBus)
        {
            _uiService = uiService;
            _eventBus = eventBus;
            _eventBus.Subscribe<ActiveHeroChosenEvent>(OnHeroChosen);
        }

        private void OnHeroChosen(ActiveHeroChosenEvent @event)
        {
            var activeHero = @event.ActiveHeroView;

            foreach (var hero in _uiService.GetRedPlayerList().GetViews())
            {
                hero.SetActive(hero == activeHero);
            }

            foreach (var hero in _uiService.GetBluePlayerList().GetViews())
            {
                hero.SetActive(hero == activeHero);
            }
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<ActiveHeroChosenEvent>(OnHeroChosen);
        }
    }
}
