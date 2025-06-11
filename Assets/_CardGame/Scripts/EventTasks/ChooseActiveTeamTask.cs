using System.Threading.Tasks;
using _CardGame.Services;
using _CardGame.Teams;
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
//             if (_taskCompletionSource != null)
//                 await _taskCompletionSource.Task;
//
//             _taskCompletionSource = new TaskCompletionSource<bool>();
//
//             // _activeTeamService.OnActiveTeamChanged += OnActiveTeamChanged;
//             _activeTeamService.ToggleActiveTeam();
//             _taskCompletionSource.SetResult(true);
// //
//             await _taskCompletionSource.Task;
//
//             // _activeTeamService.OnActiveTeamChanged -= OnActiveTeamChanged;
//             _taskCompletionSource = null;
//
            _activeTeamService.ToggleActiveTeam();
            await UniTask.Yield(); // Для совместимости с пайплайном
        }

        private void OnActiveTeamChanged(Team obj)
        {
            if (_taskCompletionSource != null && !_taskCompletionSource.Task.IsCompleted)
            {
                _taskCompletionSource.SetResult(true);
            }
        }
    }
}
