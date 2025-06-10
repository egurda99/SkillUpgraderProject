using System.Threading.Tasks;
using _CardGame.Services;
using _CardGame.Teams;

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

        public override async Task Run()
        {
            if (_taskCompletionSource != null)
                await _taskCompletionSource.Task;

            _activeTeamService.OnActiveTeamChanged += OnActiveTeamChanged;
            _taskCompletionSource = new TaskCompletionSource<bool>();
            _activeTeamService.ToggleActiveTeam();

            await _taskCompletionSource.Task;
            _taskCompletionSource = null;

            _activeTeamService.OnActiveTeamChanged -= OnActiveTeamChanged;
        }

        private void OnActiveTeamChanged(Team obj)
        {
            _taskCompletionSource.SetResult(true);
        }
    }
}
