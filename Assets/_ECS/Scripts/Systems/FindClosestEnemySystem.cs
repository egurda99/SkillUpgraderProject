using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class FindClosestEnemySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Team, Position>, Exc<Inactive, BulletTag>> _filter;
        private readonly EcsPoolInject<Target> _targetPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var currentTeam = _filter.Pools.Inc1.Get(entity);
                var currentPos = _filter.Pools.Inc2.Get(entity);

                var closestEnemyEntity = -1;
                var closestDistance = float.MaxValue;

                foreach (var enemyEntity in _filter.Value)
                {
                    if (enemyEntity == entity)
                        continue;

                    var enemyTeam = _filter.Pools.Inc1.Get(enemyEntity);
                    if (enemyTeam.Value == currentTeam.Value)
                        continue;

                    var enemyPos = _filter.Pools.Inc2.Get(enemyEntity);
                    var distance = Vector3.Distance(currentPos.Value, enemyPos.Value);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemyEntity = enemyEntity;
                    }
                }

                if (closestEnemyEntity == entity)
                    continue;

                if (closestEnemyEntity != -1)
                {
                    if (_targetPool.Value.Has(entity))
                    {
                        ref var target = ref _targetPool.Value.Get(entity);
                        target.EntityId = closestEnemyEntity;
                        target.Distance = closestDistance;
                    }
                    else
                    {
                        _targetPool.Value.Add(entity) = new Target
                        {
                            EntityId = closestEnemyEntity,
                            Distance = closestDistance
                        };
                    }
                }
                else
                {
                    if (_targetPool.Value.Has(entity))
                    {
                        _targetPool.Value.Del(entity);
                    }
                }
            }
        }
    }
}
