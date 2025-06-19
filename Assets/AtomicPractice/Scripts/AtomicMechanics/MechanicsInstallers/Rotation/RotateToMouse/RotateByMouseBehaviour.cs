using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public class RotateByMouseBehaviour : IEntityInit, IEntityUpdate
{
    private Transform _rootTransform;
    private AndExpression _canRotate;
    private ReactiveVariable<float> _rotateSpeed;

    private ReactiveVariable<bool> _isRotating;

    private readonly float _minAngleForRotate = 0.5f;
    private ReactiveVariable<Vector3> _mousePosition;


    public void Init(IEntity entity)
    {
        _rootTransform = entity.GetRootTransform();
        _rotateSpeed = entity.GetRotationSpeed();
        _isRotating = entity.GetIsRotating();
        _canRotate = entity.GetCanRotate();
        _mousePosition = entity.GetTargetPosition();
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canRotate.Value)
        {
            var direction = (_mousePosition.Value - _rootTransform.position).normalized;
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
