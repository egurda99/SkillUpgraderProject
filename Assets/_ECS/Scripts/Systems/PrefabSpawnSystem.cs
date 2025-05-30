using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client.Systems
{
    public sealed class PrefabSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;

        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab>, Exc<MoveDirection>> _filter =
            EcsWorlds.EVENTS;

        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                var position = _filter.Pools.Inc2.Get(@event).Value;
                var rotation = _filter.Pools.Inc3.Get(@event).Value;
                var prefab = _filter.Pools.Inc4.Get(@event).Value;

                var newEntity = _entityManager.Value.Create(prefab, position, rotation);

                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}
