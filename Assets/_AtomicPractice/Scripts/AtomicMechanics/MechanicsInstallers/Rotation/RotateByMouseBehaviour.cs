using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public class RotateByMouseBehaviour : IEntityInit, IEntityUpdate
{
    private Transform _root;
    private ReactiveVariable<float> _mouseDeltaX;
    private ReactiveVariable<float> _rotationSpeed;

    public void Init(IEntity entity)
    {
        _root = entity.GetRootTransform();
        _mouseDeltaX = entity.GetMouseDeltaX();
        _rotationSpeed = entity.GetRotationSpeed();
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        var rotationAmount = _mouseDeltaX.Value * _rotationSpeed.Value * deltaTime;
        Debug.Log("rotationAmount" + rotationAmount);

        _root.Rotate(0, rotationAmount, 0);
        _mouseDeltaX.Value = 0f;
    }
}
