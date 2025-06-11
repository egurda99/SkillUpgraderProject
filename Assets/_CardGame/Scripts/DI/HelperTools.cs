using _CardGame.EventTasks;
using _CardGame.Pipeline;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _CardGame.DI
{
    public class HelperTools : MonoBehaviour
    {
        [Inject] private TurnPipeline _pipeline;

        [Inject] private ChooseActiveTeamTask _chooseActiveTeamTask;
        [Inject] private ChooseActiveHeroTask _chooseActiveHeroTask;
        [Inject] private WaitForChooseTargetTask _waitForChooseTargetTask;

        [Button]
        private void RunTurnPipeline()
        {
            RunPipeline();
        }

        private async void RunPipeline()
        {
            await _pipeline.Run();
        }

        private void Start()
        {
            _pipeline.AddTask(_chooseActiveTeamTask);
            _pipeline.AddTask(_chooseActiveHeroTask);
            _pipeline.AddTask(_waitForChooseTargetTask);
        }
    }
}
