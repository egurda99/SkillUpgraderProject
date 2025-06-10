using System.Threading.Tasks;
using UI;

namespace _CardGame.EventTasks
{
    public sealed class WaitForChooseTargetTask : BaseTask
    {
        private TaskCompletionSource<bool> _taskCompletionSource;
        private readonly IEventBus _eventBus;

        private readonly HeroListView _blueHeroList;
        private HeroListView _redHeroList;

        public WaitForChooseTargetTask(IEventBus eventBus, UIService uiService)
        {
            _eventBus = eventBus;
            _blueHeroList = uiService.GetBluePlayerList();
            _redHeroList = uiService.GetRedPlayerList();
        }

        public override async Task Run()
        {
            if (_taskCompletionSource != null)
                await _taskCompletionSource.Task;

            _blueHeroList.OnHeroClicked += OnHeroClicked;

            _taskCompletionSource = new TaskCompletionSource<bool>();
            await _taskCompletionSource.Task;
            _taskCompletionSource = null;

            _blueHeroList.OnHeroClicked -= OnHeroClicked;
        }

        private void OnHeroClicked(HeroView hero)
        {
            _eventBus.RaiseEvent(new TargetChosenEvent(hero));
            _taskCompletionSource.SetResult(true);
        }
    }
}
