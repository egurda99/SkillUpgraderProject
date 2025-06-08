using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems
{
    public sealed class GameOverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GameOverRequest>> _eventFilter = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<Position>> _filter;
        private readonly EcsPoolInject<GameOverRequest> _gameOverPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Inactive> _inActivePool;

        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _eventFilter.Value)
            {
                _gameOverPool.Value.Del(@event);
                Debug.Log("<color=red>GAME OVER</color>");

                foreach (var entity in _filter.Value)
                {
                    if (!_inActivePool.Value.Has(entity))
                    {
                        _inActivePool.Value.Add(entity);
                    }
                }
            }
        }
    }
}
