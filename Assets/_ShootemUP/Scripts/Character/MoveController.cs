using System;
using ShootEmUp;
using UnityEngine;

public sealed class MoveController : IDisposable
{
    private readonly MoveComponent _moveComponent;

    private readonly IInput _input;

    public MoveController(MoveComponent moveComponent, IInput input)
    {
        _moveComponent = moveComponent;
        _input = input;
        _input.OnMoveInputChanged += Move;
    }

    void IDisposable.Dispose()
    {
        _input.OnMoveInputChanged -= Move;
    }

    private void Move(Vector2 direction) => _moveComponent.MoveByRigidbodyVelocity(direction);
}
