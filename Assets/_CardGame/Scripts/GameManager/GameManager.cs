using System;
using _CardGame.Controllers;
using _CardGame.Teams;

namespace _CardGame
{
    public sealed class GameManager : IDisposable
    {
        private readonly GameEndController _gameEndController;

        public event Action<Team> GameEnded;

        public GameManager(GameEndController gameEndController)
        {
            _gameEndController = gameEndController;
            _gameEndController.OnGameEnd += OnGameEnded;
        }

        private void OnGameEnded(Team loseTeam)
        {
            if (loseTeam == Team.Blue)
            {
                GameEnded?.Invoke(Team.Red);
            }
            else
            {
                GameEnded?.Invoke(Team.Blue);
            }
        }


        public void Dispose()
        {
            _gameEndController.OnGameEnd -= OnGameEnded;
        }
    }
}
