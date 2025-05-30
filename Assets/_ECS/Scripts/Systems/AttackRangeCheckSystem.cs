using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public sealed class AttackRangeCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Team, Position, Target, AttackRange>, Exc<Inactive, AttackBlockDuration>>
            _filter;

        private readonly EcsPoolInject<FireRequest> _fireRequestPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var target = _filter.Pools.Inc3.Get(entity);
                var attackRange = _filter.Pools.Inc4.Get(entity);

                if (target.Distance <= attackRange.Value)
                {
                    if (!_fireRequestPool.Value.Has(entity))
                    {
                        _fireRequestPool.Value.Add(entity);
                    }
                }
                else
                {
                    if (_fireRequestPool.Value.Has(entity))
                    {
                        _fireRequestPool.Value.Del(entity);
                    }
                }
            }
        }
    }
}
