using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class StatAdapterFactory
    {
        private readonly DiContainer _container;


        public StatAdapterFactory(DiContainer container)
        {
            _container = container;
        }

        public StatAdapter GetStatAdapter(CharacterStat stat, StatView statView)
        {
            var statAdapter = new StatAdapter(stat, statView);
            _container.QueueForInject(statAdapter);

            statAdapter.Initialize();

            return statAdapter;
        }
    }
}
