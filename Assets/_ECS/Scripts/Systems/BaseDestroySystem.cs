using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client.Systems
{
    public sealed class BaseDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseTag, DeathEvent>> _filter;

        private readonly EcsPoolInject<GameOverRequest> _gameOverPool = EcsWorlds.EVENTS;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;

        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _entityManager.Value.Destroy(entity);

                var gameOverEvent = _eventWorld.Value.NewEntity();
                _gameOverPool.Value.Add(gameOverEvent);
            }
        }
    }
}
