using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : IGameFixedUpdateListener
    {
        private const float MinimalDistance = 0.25f;
        private readonly MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;
        private readonly Transform _enemyTransform;

        public event Action OnPositionReached;

        public EnemyMoveAgent(MoveComponent moveComponent, Transform enemyTransform)
        {
            _moveComponent = moveComponent;
            _enemyTransform = enemyTransform;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)_enemyTransform.position;
            if (vector.magnitude <= MinimalDistance)
            {
                _isReached = true;
                OnPositionReached?.Invoke();
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }
    }
}
