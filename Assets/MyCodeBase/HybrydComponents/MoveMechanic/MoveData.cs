using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MyCodeBase.HybridComponents
{
    [Serializable]
    public sealed class MoveData
    {
        [SerializeField] private float _speed;
        [SerializeField] private bool _canMove;
        [SerializeField] private Transform _rootTransform;

        [SerializeField] private Vector3 _direction;

        [ShowInInspector] [ReadOnly] private bool _isMoving;
        private CompositeCondition _canMoveCondition = new();

        public float Speed => _speed;

        public Transform RootTransform => _rootTransform;
        public Vector3 Direction => _direction;

        public bool CanMove => _canMove;

        public CompositeCondition CanMoveCondition => _canMoveCondition;


        public bool IsMoving => _isMoving;

        public event Action<bool> OnIsMovingChanged;


        public void SetDirection(Vector3 dir)
        {
            _direction = dir;
        }

        public void SetIsMoving(bool isMoving)
        {
            _isMoving = isMoving;
            OnIsMovingChanged?.Invoke(isMoving);
        }


        [Button]
        public bool CheckCondition()
        {
            return _canMoveCondition.Invoke();
        }
    }
}
