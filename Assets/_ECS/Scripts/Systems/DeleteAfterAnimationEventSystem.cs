using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client.Systems
{
    public sealed class DeleteAfterAnimationEventSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeleteTag>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _entityManager.Value.Destroy(entity);
            }
        }
    }
}
