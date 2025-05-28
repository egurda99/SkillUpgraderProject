using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client.Systems
{
    internal sealed class BulletDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BulletTag, Inactive>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                _entityManager.Value.Destroy(entity);
            }
        }
    }
}

