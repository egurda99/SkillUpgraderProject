namespace Lessons.Architecture.PM
{
    public sealed class StatsListHandlerFactory
    {
        private readonly StatViewFactory _statViewFactory;
        private readonly StatAdapterFactory _statAdapterFactory;

        public StatsListHandlerFactory(StatViewFactory statViewFactory, StatAdapterFactory statAdapterFactory)
        {
            _statViewFactory = statViewFactory;
            _statAdapterFactory = statAdapterFactory;
        }

        public StatsListHandler Create()
        {
            return new StatsListHandler(_statViewFactory, _statAdapterFactory);
        }
    }
}
