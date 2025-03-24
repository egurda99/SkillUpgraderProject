using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : MonoBehaviour,
        IGameFixedUpdateListener
    {
        private const float MinimalDistance = 0.25f;
        private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;

        public event Action OnPositionReached;

        private void Awake()
        {
            _moveComponent = GetComponent<MoveComponent>();
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;
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
