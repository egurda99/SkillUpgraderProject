using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MyCodeBase.HybridComponents
{
    [Serializable]
    public sealed class RotationData
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private bool _canRotate;
        [SerializeField] private Transform _rootTransform;

        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _minAngleForRotate = 0.5f;

        public float MinAngleForRotate => _minAngleForRotate;

        [ShowInInspector] [ReadOnly] private bool _isRotating;
        private CompositeCondition _canRotateCondition = new();

        public float RotationSpeed => _rotationSpeed;

        public Transform RootTransform => _rootTransform;
        public Vector3 Direction => _direction;

        public bool CanRotate => _canRotate;

        public CompositeCondition CanRotateCondition => _canRotateCondition;


        public bool IsRotating => _isRotating;

        public void SetDirection(Vector3 dir)
        {
            _direction = dir;
        }

        public void SetIsRotating(bool isMoving)
        {
            _isRotating = isMoving;
        }

        [Button]
        public bool CheckCondition()
        {
            return _canRotateCondition.Invoke();
        }
    }
}
