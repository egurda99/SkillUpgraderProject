using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class MeleeAttackActionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MeleeAttackAction, MeleeWeapon, Target>, Exc<Inactive>>
            _filter;

        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<MeleeAttackAction> _meleeAttackActionPool;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var targetPool = _filter.Pools.Inc3;
            var weaponPool = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                if (!targetPool.Has(entity))
                    continue;

                var targetEntity = targetPool.Get(entity).EntityId;
                if (!world.IsEntityAliveInternal(targetEntity) || !_healthPool.Value.Has(targetEntity))
                    continue;

                var damage = weaponPool.Get(entity).Damage;

                ref var health = ref _healthPool.Value.Get(targetEntity).Value;
                health = Mathf.Max(0, health - (int)damage);

                Debug.Log($"[MeleeAttack] Entity {entity} hit {targetEntity} for {damage}");

                _meleeAttackActionPool.Value.Del(entity);
            }
        }
    }
}
