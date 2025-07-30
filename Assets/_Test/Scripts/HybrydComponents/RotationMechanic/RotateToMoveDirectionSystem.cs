using UnityEngine;

namespace MyCodeBase.HybridComponents
{
    public sealed class RotateToMoveDirectionSystem
    {
        private readonly MoveData _moveData;
        private readonly RotationData _rotationData;
        private readonly Transform _rootTransform;

        public RotateToMoveDirectionSystem(RotationData rotationData, MoveData moveData)
        {
            _moveData = moveData;
            _rotationData = rotationData;

            _rotationData.CanRotateCondition.AddCondition(() => _rotationData.CanRotate);

            _rootTransform = _rotationData.RootTransform;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_rotationData.CanRotateCondition.Invoke())
                return;

            var moveDirection = _moveData.Direction;

            if (moveDirection.sqrMagnitude < 0.001f)
                return;

            var targetRotation = Quaternion.LookRotation(moveDirection);
            var angle = Quaternion.Angle(_rootTransform.rotation, targetRotation);

            if (angle > _rotationData.MinAngleForRotate)
            {
                _rotationData.SetIsRotating(true);

                _rootTransform.rotation = Quaternion.RotateTowards(
                    _rootTransform.rotation,
                    targetRotation,
                    _rotationData.RotationSpeed * deltaTime
                );
            }
            else
            {
                _rotationData.SetIsRotating(false);
            }
        }
    }
}
