using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class StatViewFactory
    {
        private readonly Transform _container;
        private readonly StatView _statPrefab;

        public StatViewFactory(Transform container, StatView statPrefab)
        {
            _container = container;
            _statPrefab = statPrefab;
        }

        public StatView GetStatView()
        {
            return Object.Instantiate(_statPrefab, _container);
        }
    }
}