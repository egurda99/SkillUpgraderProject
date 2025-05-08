using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class RotateBehaviour : IEntityInit, IEntityUpdate
{
    private Transform _rootTransform;
    private AndExpression _canRotate;
    private ReactiveVariable<float> _rotateSpeed;
    private ReactiveVariable<bool> _isRotating;
    private ReactiveVariable<Transform> _target;
    private readonly float _minAngleForRotate = 0.5f;


    public void Init(IEntity entity)
    {
        _rootTransform = entity.GetRootTransform();
        _rotateSpeed = entity.GetRotationSpeed();
        _isRotating = entity.GetIsRotating();
        _canRotate = entity.GetCanRotate();
        _target = entity.GetTarget();
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canRotate.Value)
        {
            Vector3 direction = (_target.Value.position - _rootTransform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float angle = Quaternion.Angle(_rootTransform.rotation, targetRotation);

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
