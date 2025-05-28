﻿using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class ExampleSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<MoveDirection> _moveDirectionPool;
        private readonly EcsPoolInject<MoveSpeed> _moveSpeedPool;
        
        public void Init(IEcsSystems systems)
        {
            int newEntity = systems.GetWorld().NewEntity();

            _positionPool.Value.Add(newEntity) = new Position{ Value = Vector3.zero };
            _moveDirectionPool.Value.Add(newEntity) = new MoveDirection{ Value = Vector3.up };
            _moveSpeedPool.Value.Add(newEntity) = new MoveSpeed{ Value = 5.0f};
        }
    }
}
