using System.Threading.Tasks;
using _CardGame.Services;
using Cysharp.Threading.Tasks;

namespace _CardGame.EventTasks
{
    public sealed class ChooseActiveTeamTask : BaseTask
    {
        private TaskCompletionSource<bool> _taskCompletionSource;
        private readonly ActiveTeamService _activeTeamService;

        public ChooseActiveTeamTask(ActiveTeamService activeTeamService)
        {
            _activeTeamService = activeTeamService;
        }

        public override async UniTask Run()
        {
            _activeTeamService.ToggleActiveTeam();
            await UniTask.Yield();
        }
    }
}
