using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position>, Exc<Inactive>> _filter;

        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;

            #region MyRegion

            // EcsWorld world = systems.GetWorld();
            // EcsFilter filter = world.Filter<MoveDirection>().Inc<MoveSpeed>().Inc<Position>().Exc<Inactive>().End();
            //
            // EcsPool<MoveDirection> directionPool = world.GetPool<MoveDirection>();
            // EcsPool<MoveSpeed> speedPool = world.GetPool<MoveSpeed>();
            // EcsPool<Position> positionPool = world.GetPool<Position>();

            #endregion

            var directionPool = _filter.Pools.Inc1;
            var speedPool = _filter.Pools.Inc2;
            var positionPool = _filter.Pools.Inc3;

            foreach (var entity in _filter.Value)
            {
                var moveDirection = directionPool.Get(entity);
                var moveSpeed = speedPool.Get(entity);
                ref var position = ref positionPool.Get(entity);
                position.Value += moveDirection.Value * (moveSpeed.Value * deltaTime);
            }
        }
    }
}
