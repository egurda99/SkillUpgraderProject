using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;
        private readonly float _minimalDistance = 0.25f;

        public event Action OnPositionReached;

        private void Awake()
        {
            _moveComponent = GetComponent<MoveComponent>();
        }

        private void FixedUpdate()
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;

            if (vector.magnitude <= _minimalDistance)
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
