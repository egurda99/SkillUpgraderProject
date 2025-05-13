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
            _characterStatsHolder.OnStatRemoved
                .Subscribe(OnStatRemoved)
                .AddTo(_disposable);

            _characterStatsHolder.OnStatAdded
                .Subscribe(OnStatAdded)
                .AddTo(_disposable);


            foreach (var stat in characterStatsHolder.Stats.Values)
            {
                _listHandler.AddStat(stat);
            }
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
            _listHandler.Clear();
        }


        private void OnStatRemoved(CharacterStat stat)
        {
            _listHandler.RemoveStat(stat);
        }

        private void OnStatAdded(CharacterStat stat)
        {
            _listHandler.AddStat(stat);
        }
    }
}
