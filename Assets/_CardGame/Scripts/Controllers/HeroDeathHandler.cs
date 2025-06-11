using System.Linq;
using _CardGame.Installers;
using _CardGame.Teams;
using UI;

namespace _CardGame.Controllers
{
    public sealed class HeroDeathHandler
    {
        private readonly HeroView _view;
        private readonly UIService _uiService;
        private readonly HeroListView _redTeam;
        private readonly HeroListView _blueTeam;
        private readonly Team? _currentTeam;
        private readonly CardInstaller _cardInstaller;


        public HeroDeathHandler(HeroView view, UIService uiService)
        {
            _view = view;
            _redTeam = uiService.GetRedPlayerList();
            _blueTeam = uiService.GetBluePlayerList();

            _currentTeam = GetTeamForHero(_view);

            _cardInstaller = _view.gameObject.GetComponent<CardInstaller>();
            _cardInstaller.CardView.HealthData.OnDeath += OnHeroDeath;
        }

        private void OnHeroDeath()
        {
            if (_currentTeam == null)
                return;

            if (_currentTeam == Team.Blue)
            {
                _blueTeam.Remove(_view);
            }

            else
            {
                _redTeam.Remove(_view);
            }

            _cardInstaller.CardView.HealthData.OnDeath -= OnHeroDeath;
        }

        public Team? GetTeamForHero(HeroView view)
        {
            if (_redTeam.GetViews().Contains(view))
                return Team.Red;

            if (_blueTeam.GetViews().Contains(view))
                return Team.Blue;

            return null; // Не найден — возможно, уже удалён
        }
    }
}
