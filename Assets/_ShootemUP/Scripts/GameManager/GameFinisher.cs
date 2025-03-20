using ShootemUP;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameFinisher
    {
        private readonly GameCycleManager _gameCycleManager;

        public GameFinisher(GameCycleManager gameCycleManager)
        {
            _gameCycleManager = gameCycleManager;
        }

        public void FinishGame()
        {
            Debug.Log("<color=red>GAME OVER</color>");

            _gameCycleManager.FinishGame();
            //Time.timeScale = 0;
        }
    }
}
