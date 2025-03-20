using System;
using ShootemUP;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : MonoBehaviour,
        IGameFixedUpdateListener
    {
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
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                OnPositionReached?.Invoke();
                Debug.Log("<color=red>ReachedEventMoveAgent</color>");
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
