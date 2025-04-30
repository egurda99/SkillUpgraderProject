using R3;

namespace Lessons.Architecture.PM
{
    public sealed class StatListAdapter
    {
        private readonly StatsListHandler _listHandler;
        private readonly CharacterStatsHolder _characterStatsHolder;
        private readonly CompositeDisposable _disposable = new();

        public StatListAdapter(StatsListHandler listHandler, CharacterStatsHolder characterStatsHolder)
        {
            _listHandler = listHandler;
            _characterStatsHolder = characterStatsHolder;
        }

        public void Initialize()
        {
            _characterStatsHolder.OnStatRemoved
                .Subscribe(OnStatRemoved)
                .AddTo(_disposable);

            _characterStatsHolder.OnStatAdded
                .Subscribe(OnStatAdded)
                .AddTo(_disposable);
        }


        public void Dispose()
        {
            _disposable.Dispose();
            _listHandler.Clear();
        }

        public void Show()
        {
            _listHandler.Show();
        }

        public void Hide()
        {
            _listHandler.Hide();
        }


        private void OnStatRemoved(CharacterStat stat)
        {
            _listHandler.RemoveStat(stat);
        }

        private void OnStatAdded(CharacterStat stat)
        {
            _listHandler.AddStat(stat);
        }

        public void Reload()
        {
            _listHandler.Clear();

            foreach (var stat in _characterStatsHolder.GetStats())
            {
                _listHandler.AddStat(stat);
            }
        }
    }
}
