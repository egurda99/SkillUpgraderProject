using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public sealed class MeleeAttackRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest, MeleeWeapon, Target>, Exc<Inactive, AttackBlockDuration>>
            _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<MeleeAttackEvent> _meleeAttackEventPool = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<TargetEntity> _targetEntityPool = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<Damage> _damagePool = EcsWorlds.EVENTS;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var positionPool = world.GetPool<Position>();
            var attackCooldownPool = world.GetPool<AttackCooldown>();
            var canAttackPool = world.GetPool<CanAttack>();

            var blockAttackPool = world.GetPool<AttackBlockDuration>();

            var targetPool = _filter.Pools.Inc3;
            var weaponPool = _filter.Pools.Inc2;
            var requestPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {
                var damage = weaponPool.Get(entity);
                var target = targetPool.Get(entity);

                if (!positionPool.Has(target.EntityId))
                {
                    requestPool.Del(entity);
                    continue;
                }

                var targetEntity = target.EntityId;

                var targetPosition = positionPool.Get(targetEntity).Value;

                var spawnEvent = _eventWorld.Value.NewEntity();

                _meleeAttackEventPool.Value.Add(spawnEvent);
                _targetEntityPool.Value.Add(spawnEvent) = new TargetEntity { Value = entity };
                _damagePool.Value.Add(spawnEvent) = new Damage { Value = (int)damage.Damage };

                blockAttackPool.Add(entity) = new AttackBlockDuration { Timer = attackCooldownPool.Get(entity).Value };

                requestPool.Del(entity);

                if (canAttackPool.Has(entity))
                {
                    canAttackPool.Del(entity);
                }
            }
        }
    }
}
