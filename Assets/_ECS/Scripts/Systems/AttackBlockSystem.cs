using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class AttackBlockSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackBlockDuration>, Exc<Inactive>> _filter;

        public void Run(IEcsSystems systems)
        {
            var durationPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {
                ref var block = ref durationPool.Get(entity);

                block.Timer -= Time.deltaTime;
                if (block.Timer <= 0)
                {
                    durationPool.Del(entity);
                }
            }
        }
    }
}