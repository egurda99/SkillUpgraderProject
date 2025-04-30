namespace Lessons.Architecture.PM
{
    public sealed class CharacterStatsSectionViewModel : ISectionViewModel
    {
        private readonly CharacterStatsHolder _statsHolderModel;
        private readonly StatListAdapter _statListAdapter;

        public CharacterStatsSectionViewModel(CharacterStatsHolder statsHolder,
            StatsListViewFactory statsListViewFactory)
        {
            _statsHolderModel = statsHolder;
            _statListAdapter = new StatListAdapter(statsListViewFactory.Create(), statsHolder);
            _statListAdapter.Initialize();
        }

        public void Show()
        {
            _statListAdapter.Show();
        }

        public void Hide()
        {
            _statListAdapter.Hide();
        }

        public void Dispose()
        {
            _statListAdapter.Dispose();
        }
    }
}
