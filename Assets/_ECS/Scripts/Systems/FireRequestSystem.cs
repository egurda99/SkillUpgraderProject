using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public sealed class FireRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FireRequest, BulletWeapon, Target>, Exc<Inactive, AttackBlockDuration>>
            _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<SpawnRequest> _spawnPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<MoveDirection> _directionPool = EcsWorlds.EVENTS;


        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld(); // Основной мир (не events!)
            var positionPool = world.GetPool<Position>();
            var attackCooldownPool = world.GetPool<AttackCooldown>();

            var blockAttackPool = world.GetPool<AttackBlockDuration>();

            var targetPool = _filter.Pools.Inc3;
            var weaponPool = _filter.Pools.Inc2;
            var requestPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {
                var weapon = weaponPool.Get(entity);
                var target = targetPool.Get(entity);

                if (!positionPool.Has(target.EntityId))
                {
                    _filter.Pools.Inc1.Del(entity);
                    continue;
                }

                var targetEntity = target.EntityId;


                var targetPosition = positionPool.Get(targetEntity).Value;

                var spawnEvent = _eventWorld.Value.NewEntity();

                _spawnPool.Value.Add(spawnEvent) = new SpawnRequest();
                _positionPool.Value.Add(spawnEvent) = new Position { Value = weapon.FirePoint.position };
                _rotationPool.Value.Add(spawnEvent) = new Rotation { Value = weapon.FirePoint.rotation };
                _prefabPool.Value.Add(spawnEvent) = new Prefab { Value = weapon.BulletPrefab };
                _directionPool.Value.Add(spawnEvent) = new MoveDirection
                    { Value = (targetPosition - weapon.FirePoint.position).normalized };

                blockAttackPool.Add(entity) = new AttackBlockDuration { Timer = attackCooldownPool.Get(entity).Value };

                requestPool.Del(entity);
            }
        }
    }
}
