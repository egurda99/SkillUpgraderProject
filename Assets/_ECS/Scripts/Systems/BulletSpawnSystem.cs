using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems
{
    public sealed class BulletSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;

        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab, MoveDirection>> _filter =
            EcsWorlds.EVENTS;

        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            var mainWorld = systems.GetWorld();
            foreach (var @event in _filter.Value)
            {
                var prefab = _filter.Pools.Inc4.Get(@event).Value;
                var position = _filter.Pools.Inc2.Get(@event).Value;
                var rotation = _filter.Pools.Inc3.Get(@event).Value;
                var direction = _filter.Pools.Inc5.Get(@event).Value;

                var lookRotation = Quaternion.LookRotation(direction);
                var bulletEntity = _entityManager.Value.Create(prefab, position, lookRotation);

                var movePool = mainWorld.GetPool<MoveDirection>();

                movePool.Add(bulletEntity.Id) = new MoveDirection { Value = direction };

                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}
