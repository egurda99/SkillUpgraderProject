using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class AttackRangeCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Team, Position, Target, AttackRange>, Exc<Inactive>> _filter;

        private readonly EcsPoolInject<AttackRequest> _attackRequestPool;
        private readonly EcsPoolInject<CanAttack> _canAttackPool;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<Target> _targetPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var currentPosition = _filter.Pools.Inc2.Get(entity);
                var target = _filter.Pools.Inc3.Get(entity);
                var attackRange = _filter.Pools.Inc4.Get(entity);

                var targetPosition = _positionPool.Value.Get(target.EntityId);
                var distance = Vector3.Distance(targetPosition.Value, currentPosition.Value);

                if (distance <= attackRange.Value)
                {
                    if (!_attackRequestPool.Value.Has(entity))
                    {
                        _attackRequestPool.Value.Add(entity);
                    }

                    if (!_canAttackPool.Value.Has(entity))
                    {
                        _canAttackPool.Value.Add(entity);
                    }
                }
                else
                {
                    if (_attackRequestPool.Value.Has(entity))
                    {
                        _attackRequestPool.Value.Del(entity);
                    }

                    if (_canAttackPool.Value.Has(entity))
                    {
                        _canAttackPool.Value.Del(entity);
                    }
                }
            }
        }
    }
}
