using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class RotateToMoveDirectionBehaviour : IEntityInit, IEntityUpdate
{
    private Transform _rootTransform;
    private AndExpression _canRotate;
    private ReactiveVariable<float> _rotateSpeed;

    private ReactiveVariable<bool> _isRotating;

    private readonly float _minAngleForRotate = 0.5f;
    private ReactiveVariable<Vector3> _moveDirection;


    public void Init(IEntity entity)
    {
        _rootTransform = entity.GetRootTransform();
        _rotateSpeed = entity.GetRotationSpeed();
        _isRotating = entity.GetIsRotating();
        _canRotate = entity.GetCanRotate();

        _moveDirection = entity.GetMoveDirection();
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canRotate.Value)
        {
            var direction = _moveDirection.Value;

            if (direction == Vector3.zero)
            {
                _isRotating.Value = false;
                return;
            }

            var targetRotation = Quaternion.LookRotation(direction);
            var angle = Quaternion.Angle(_rootTransform.rotation, targetRotation);

            if (angle > _minAngleForRotate)
            {
                _isRotating.Value = true;

                _rootTransform.rotation = Quaternion.RotateTowards(_rootTransform.rotation,
                    targetRotation,
                    _rotateSpeed.Value * deltaTime);
            }

            else
            {
                _isRotating.Value = false;
            }
        }
    }
}
