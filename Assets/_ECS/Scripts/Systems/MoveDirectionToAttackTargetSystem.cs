using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class MoveDirectionToAttackTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, Target>, Exc<Inactive>> _filter;

        private readonly EcsPoolInject<MoveDirection> _moveDirectionPool;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<Target> _targetPool;
        private readonly EcsPoolInject<CanAttack> _canAttackPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var currentPosition = _filter.Pools.Inc1.Get(entity);
                var target = _filter.Pools.Inc2.Get(entity);

                if (_canAttackPool.Value.Has(entity))
                {
                    _moveDirectionPool.Value.Get(entity).Value = Vector3.zero;
                    continue;
                }

                var targetPos = _positionPool.Value.Get(target.EntityId).Value;
                var direction = (targetPos - currentPosition.Value).normalized;

                if (_moveDirectionPool.Value.Has(entity))
                {
                    _moveDirectionPool.Value.Get(entity).Value = direction;
                }
                else
                {
                    _moveDirectionPool.Value.Add(entity) = new MoveDirection { Value = direction };
                }
            }
        }
    }
}
