using System;
using ShootEmUp;
using UnityEngine;
using Zenject;

public sealed class MoveController : IDisposable
{
    private readonly MoveComponent _moveComponent;

    private IInput _input;

    public MoveController(MoveComponent moveComponent)
    {
        _moveComponent = moveComponent;
    }

    [Inject]
    public void Construct(IInput input)
    {
        _input = input;
        _input.OnMoveInputChanged += Move;
    }

    public void Dispose()
    {
        Debug.Log("Dispose Move Controller");
        _input.OnMoveInputChanged -= Move;
    }

    private void Move(Vector2 direction) => _moveComponent.MoveByRigidbodyVelocity(direction);
}
