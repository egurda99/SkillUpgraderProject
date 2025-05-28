using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class TakeDamageRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageRequest, TargetEntity, Damage>, Exc<Inactive>> _filter =
            EcsWorlds.EVENTS;

        private readonly EcsPoolInject<TakeDamageEvent> _eventPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<OneFrame> _oneFramePool = EcsWorlds.EVENTS;

        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<Health> _healthPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                var target = _filter.Pools.Inc2.Get(@event).Value;
                var damage = _filter.Pools.Inc3.Get(@event).Value;

                if (_world.Value.IsEntityAliveInternal(target) && _healthPool.Value.Has(target))
                {
                    ref var health = ref _healthPool.Value.Get(target).Value;
                    health = Mathf.Max(0, health - damage);
                }

                _filter.Pools.Inc1.Del(@event);
                _eventPool.Value.Add(@event) = new TakeDamageEvent();
                _oneFramePool.Value.Add(@event) = new OneFrame();
            }
        }
    }
}
