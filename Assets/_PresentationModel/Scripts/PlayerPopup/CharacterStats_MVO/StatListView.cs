using System;
using R3;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class StatListView : IInitializable, IDisposable
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
