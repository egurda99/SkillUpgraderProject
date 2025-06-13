using System.Threading.Tasks;
using _CardGame.Events;
using _CardGame.Services;
using _CardGame.Teams;
using Cysharp.Threading.Tasks;
using UI;

namespace _CardGame.EventTasks
{
    public sealed class WaitForChooseTargetTask : BaseTask
    {
        private TaskCompletionSource<bool> _taskCompletionSource;
        private readonly IEventBus _eventBus;


        private HeroListView _activeHeroList;
        private readonly ActiveTeamService _activeTeamService;
        private readonly UIService _uiService;

        public WaitForChooseTargetTask(IEventBus eventBus, UIService uiService, ActiveTeamService activeTeamService)
        {
            _eventBus = eventBus;
            _activeTeamService = activeTeamService;
            _uiService = uiService;
        }

        public override async UniTask Run()
        {
            if (_taskCompletionSource != null)
                await _taskCompletionSource.Task;

            if (_activeTeamService.ActiveTeam == Team.Red)
            {
                _activeHeroList = _uiService.GetBluePlayerList();
            }

            else
            {
                _activeHeroList = _uiService.GetRedPlayerList();
            }


            _activeHeroList.OnHeroClicked += OnHeroClicked;

            _taskCompletionSource = new TaskCompletionSource<bool>();
            await _taskCompletionSource.Task;
            _taskCompletionSource = null;

            _activeHeroList.OnHeroClicked -= OnHeroClicked;
        }

        private void OnHeroClicked(HeroView hero)
        {
            _eventBus.RaiseEvent(new TargetChosenEvent(hero));
            _taskCompletionSource.SetResult(true);
        }
    }
}
