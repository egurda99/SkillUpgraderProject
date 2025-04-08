using System;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class StatListViewAdapter : IInitializable, IDisposable
    {
        private readonly StatsListView _listView;
        private readonly CharacterStatsHolder _characterStatsHolder;

        public StatListViewAdapter(StatsListView listView, CharacterStatsHolder characterStatsHolder)
        {
            _listView = listView;
            _characterStatsHolder = characterStatsHolder;
        }

        public void Initialize()
        {
            _characterStatsHolder.OnStatAdded += OnStatAdded;
            _characterStatsHolder.OnStatRemoved += OnStatRemoved;
        }


        public void Dispose()
        {
            _characterStatsHolder.OnStatAdded -= OnStatAdded;
            _characterStatsHolder.OnStatRemoved -= OnStatRemoved;
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
    }
}
