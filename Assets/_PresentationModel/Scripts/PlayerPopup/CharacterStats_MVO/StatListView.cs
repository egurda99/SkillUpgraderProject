using R3;

namespace Lessons.Architecture.PM
{
    public sealed class StatListView
    {
        private readonly StatsListView _listView;
        private readonly CharacterStatsHolder _characterStatsHolder;
        private readonly CompositeDisposable _disposable = new();

        public StatListView(StatsListView listView, CharacterStatsHolder characterStatsHolder)
        {
            _listView = listView;
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
            _listView.Clear();
        }

        public void Show()
        {
            _listView.Show();
        }

        public void Hide()
        {
            _listView.Hide();
        }


        private void OnStatRemoved(CharacterStat stat)
        {
            _listView.RemoveStat(stat);
        }

        private void OnStatAdded(CharacterStat stat)
        {
            _listView.AddStat(stat);
        }

        public void Reload()
        {
            _listView.Clear();

            foreach (var stat in _characterStatsHolder.GetStats())
            {
                _listView.AddStat(stat);
            }
        }
    }
}
