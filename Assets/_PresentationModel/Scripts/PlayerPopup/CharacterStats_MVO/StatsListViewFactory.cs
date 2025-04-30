namespace Lessons.Architecture.PM
{
    public sealed class StatsListViewFactory
    {
        private readonly StatViewFactory _statViewFactory;
        private readonly StatAdapterFactory _statAdapterFactory;

        public StatsListViewFactory(StatViewFactory statViewFactory, StatAdapterFactory statAdapterFactory)
        {
            _statViewFactory = statViewFactory;
            _statAdapterFactory = statAdapterFactory;
        }

        public StatsListView Create()
        {
            return new StatsListView(_statViewFactory, _statAdapterFactory);
        }
    }
}