using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    internal sealed class AnimatorMeleeAttackListenerSystem : IEcsRunSystem
    {
        private static readonly int MeleeAttackTrigger = Animator.StringToHash("MeleeAttack");

        private readonly EcsFilterInject<Inc<MeleeAttackEvent, TargetEntity>, Exc<Inactive>> _filter = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<AnimatorView> _animatorPool;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<MeleeAttackEvent> _meleeAttackPool = EcsWorlds.EVENTS;


        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                var target = _targetEntityPool.Value.Get(@event).Value;

                if (_animatorPool.Value.Has(target))
                {
                    var animator = _animatorPool.Value.Get(target).Value;
                    animator.SetTrigger(MeleeAttackTrigger);
                }

                _meleeAttackPool.Value.Del(@event);
            }
        }
    }
}
