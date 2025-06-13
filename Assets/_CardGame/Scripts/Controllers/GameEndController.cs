using System;
using UI;

namespace _CardGame.Controllers
{
    public sealed class GameEndController : IDisposable
    {
        private readonly UIService _uiService;

        public event Action<Team> OnGameEnd;

        public GameEndController(UIService uiService)
        {
            _uiService = uiService;

            _uiService.GetBluePlayerList().OnAllHeroesDead += OnBlueTeamDead;
            _uiService.GetRedPlayerList().OnAllHeroesDead += OnRedTeamDead;
        }

        private void OnRedTeamDead()
        {
            OnGameEnd?.Invoke(Team.Blue);
        }

        private void OnBlueTeamDead()
        {
            OnGameEnd?.Invoke(Team.Red);
        }

        public void Dispose()
        {
            _uiService.GetBluePlayerList().OnAllHeroesDead -= OnBlueTeamDead;
            _uiService.GetRedPlayerList().OnAllHeroesDead -= OnRedTeamDead;
        }
    }
}
