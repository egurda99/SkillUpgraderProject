using System;
using _CardGame.Teams;
using UnityEngine;

namespace _CardGame
{
    public sealed class GameEndViewAdapter : IDisposable
    {
        private readonly GameManager _gameManager;
        private readonly GameEndView _gameEndView;

        public GameEndViewAdapter(GameManager gameManager, GameEndView gameEndView)
        {
            _gameManager = gameManager;
            _gameEndView = gameEndView;

            _gameManager.GameEnded += OnGameEnded;
        }

        private void OnGameEnded(Team winTeam)
        {
            if (winTeam == Team.Blue)
            {
                _gameEndView.SetWinText("Победила команда синих!");
                _gameEndView.SetWinColor(new Color32(0x00, 0x6F, 0xFA, 0x80));
                _gameEndView.Show();
            }

            else
            {
                _gameEndView.SetWinText("Победила команда красных!");
                _gameEndView.SetWinColor(new Color32(0xFA, 0x00, 0x00, 0x80));
                _gameEndView.Show();
            }
        }


        public void Dispose()
        {
            _gameManager.GameEnded -= OnGameEnded;
        }
    }
}
