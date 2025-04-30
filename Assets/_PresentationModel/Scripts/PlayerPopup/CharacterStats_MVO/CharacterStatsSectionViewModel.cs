namespace Lessons.Architecture.PM
{
    public sealed class CharacterStatsSectionViewModel : ISectionViewModel
    {
        private readonly CharacterStatsHolder _statsHolderModel;
        private readonly StatListAdapter _statListAdapter;

        public CharacterStatsSectionViewModel(CharacterStatsHolder statsHolder,
            StatsListHandlerFactory statsListHandlerFactory)
        {
            _statsHolderModel = statsHolder;
            _statListAdapter = new StatListAdapter(statsListHandlerFactory.Create(), statsHolder);
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
