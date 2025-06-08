using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    internal sealed class AnimatorMoveListenerSystem : IEcsRunSystem
    {
        private static readonly int _moveValue = Animator.StringToHash("isMoving");

        private readonly EcsFilterInject<Inc<AnimatorView, MoveDirection>, Exc<Inactive>> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var animator = _filter.Pools.Inc1.Get(entity).Value;
                var direction = _filter.Pools.Inc2.Get(entity).Value;

                if (direction.sqrMagnitude > 0.1f)
                {
                    animator.SetBool(_moveValue, true);
                }

                else
                {
                    animator.SetBool(_moveValue, false);
                }
            }
        }
    }
}
