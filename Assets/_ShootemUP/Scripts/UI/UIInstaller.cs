using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class UIInstaller : MonoBehaviour
    {
        [SerializeField] private StartGameWidget _startGameWidget;
        [SerializeField] private PauseGameWidget _pauseGameWidget;
        [SerializeField] private TimerBeforeStartWidget _timerBeforeStartWidget;

        private GameCycleWidgetsHandler _gameCycleWidgetsHandler;
        private GameCycleManager _gameCycleManager;


        [Inject]
        public void Construct(GameCycleManager gameCycleManager)
        {
            _gameCycleManager = gameCycleManager;
        }

        private void Awake()
        {
            _gameCycleWidgetsHandler =
                new GameCycleWidgetsHandler(_startGameWidget, _pauseGameWidget, _gameCycleManager,
                    _timerBeforeStartWidget);
        }
    }
}
