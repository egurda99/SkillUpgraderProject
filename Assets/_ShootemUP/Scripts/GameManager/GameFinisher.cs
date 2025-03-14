using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameFinisher
    {
        public void FinishGame()
        {
            Debug.Log("<color=red>GAME OVER</color>");
            Time.timeScale = 0;
        }
    }
}
