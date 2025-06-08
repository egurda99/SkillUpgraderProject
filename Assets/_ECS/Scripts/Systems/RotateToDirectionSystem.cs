using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class RotateToDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, Rotation>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<Rotation> _rotationPool;

        public void Run(IEcsSystems systems)
        {
            var directionPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {
                var moveDirection = directionPool.Get(entity);
                if (moveDirection.Value.sqrMagnitude > 0.01f)
                {
                    ref var rotation = ref _rotationPool.Value.Get(entity);

                    var direction = moveDirection.Value;
                    direction.y = 0f;

                    if (direction.sqrMagnitude > 0.001f)
                    {
                        rotation.Value = Quaternion.LookRotation(direction.normalized);
                    }
                }
            }
        }
    }
}
