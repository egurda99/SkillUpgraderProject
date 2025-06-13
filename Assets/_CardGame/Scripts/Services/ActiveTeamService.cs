using System;

namespace _CardGame.Services
{
    public sealed class ActiveTeamService
    {
        private Team _activeTeam;
        private Team _lastActiveTeam;


        public Team ActiveTeam => _activeTeam;

        public Team LastActiveTeam => _lastActiveTeam;

        public event Action<Team> OnActiveTeamChanged;


        public ActiveTeamService()
        {
            _activeTeam = Team.Red;
            _lastActiveTeam = Team.Blue;
        }

        public void ToggleActiveTeam()
        {
            _lastActiveTeam = _activeTeam;
            _activeTeam = _activeTeam == Team.Red ? Team.Blue : Team.Red;
            OnActiveTeamChanged?.Invoke(_activeTeam);
        }
    }
}
