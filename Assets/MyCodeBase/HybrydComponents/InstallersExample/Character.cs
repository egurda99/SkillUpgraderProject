using UnityEngine;

namespace MyCodeBase.HybridComponents
{
    public sealed class Character : MonoBehaviour
    {
        private MoveData _moveData;
        private RotationData _rotationData;
        [SerializeField] private HealthData _healthData;


        private MoveByTransformSystem _moveByTransformSystem;
        private RotateToMoveDirectionSystem _rotateToMoveDirectionSystem;
        private HealthSystem _healthSystem;


        public MoveData MoveData => _moveData;
        public HealthData HealthData => _healthData;

        private void Start()
        {
            _moveByTransformSystem = new MoveByTransformSystem(_moveData);
            _rotateToMoveDirectionSystem = new RotateToMoveDirectionSystem(_rotationData, _moveData);
            _healthSystem = new HealthSystem(_healthData);

            _moveData.CanMoveCondition.AddCondition(() => _healthData.IsAlive);
            _rotationData.CanRotateCondition.AddCondition(() => _healthData.IsAlive);
        }

        private void Update()
        {
            _moveByTransformSystem.OnUpdate(Time.deltaTime);
            _rotateToMoveDirectionSystem.OnUpdate(Time.deltaTime);
        }
    }
}
