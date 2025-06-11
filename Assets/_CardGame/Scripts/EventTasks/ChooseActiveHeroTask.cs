using System.Collections.Generic;
using System.Threading.Tasks;
using _CardGame.Services;
using _CardGame.Teams;
using Cysharp.Threading.Tasks;
using UI;

namespace _CardGame.EventTasks
{
    public sealed class ChooseActiveHeroTask : BaseTask
    {
        private TaskCompletionSource<bool> _taskCompletionSource;
        private readonly IEventBus _eventBus;
        private readonly HeroListView _blueHeroList;

        private HeroView _previousRed;
        private HeroView _previousBlue;

        private IReadOnlyList<HeroView> _views;
        private readonly HeroListView _redHeroList;
        private readonly ActiveTeamService _activeTeamService;
        private readonly ActiveCardService _activeCardService;

        public ChooseActiveHeroTask(IEventBus eventBus, UIService uiService, ActiveTeamService activeTeamService,
            ActiveCardService activeCardService)
        {
            _eventBus = eventBus;
            _blueHeroList = uiService.GetBluePlayerList();
            _redHeroList = uiService.GetRedPlayerList();
            _activeTeamService = activeTeamService;
            _activeCardService = activeCardService;
        }

        public override async UniTask Run()
        {
            // if (_taskCompletionSource != null)
            //     await _taskCompletionSource.Task;

            if (_activeTeamService.ActiveTeam == Team.Red)
            {
                _views = _redHeroList.GetViews();
            }

            else
            {
                _views = _blueHeroList.GetViews();
            }

            _activeCardService.SetActiveHeroView(GetNextHero());

            _eventBus.RaiseEvent(new ActiveHeroChosenEvent(_activeCardService.ActiveHeroView));

            // _taskCompletionSource = new TaskCompletionSource<bool>();
            // _taskCompletionSource.SetResult(true);
            // await _taskCompletionSource.Task;
            // _taskCompletionSource = null;
            await UniTask.Yield();
        }

        public HeroView GetNextHero()
        {
            if (_views == null || _views.Count == 0)
                return null;

            var previousForTeam = _activeTeamService.ActiveTeam == Team.Red ? _previousRed : _previousBlue;

            var startIndex = 0;

            if (previousForTeam != null)
            {
                var prevIndex = -1;
                for (var i = 0; i < _views.Count; i++)
                {
                    if (_views[i] == previousForTeam)
                    {
                        prevIndex = i;
                        break;
                    }
                }

                startIndex = (prevIndex + 1) % _views.Count;
            }

            var nextHero = _views[startIndex];

            if (_activeTeamService.ActiveTeam == Team.Red)
                _previousRed = nextHero;
            else
                _previousBlue = nextHero;

            return nextHero;
        }
    }
}
