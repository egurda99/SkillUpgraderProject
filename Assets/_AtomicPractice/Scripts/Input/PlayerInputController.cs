using System;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class PlayerInputController : IContextInit, IContextDispose
{
    [SerializeField] private SceneEntity _sceneEntity;

    private KeyboardInput _keyboardInput;
    private ReactiveVariable<Vector3> _moveDirection;
    private IEvent _shootRequest;

    public void Init(IContext context)
    {
        _keyboardInput = context.GetKeyboardInput();

        _moveDirection = _sceneEntity.GetMoveDirection();
        _shootRequest = _sceneEntity.GetShootRequest();

        _keyboardInput.OnMoveInputChanged += OnMoveInputChanged;
        _keyboardInput.OnFireClicked += OnFireClicked;
    }

    private void OnFireClicked()
    {
        _shootRequest?.Invoke();
    }

    private void OnMoveInputChanged(Vector3 direction)
    {
        _moveDirection.Value = direction;
    }

    public void Dispose(IContext context)
    {
        _keyboardInput.OnMoveInputChanged -= OnMoveInputChanged;
        _keyboardInput.OnFireClicked -= OnFireClicked;
    }
}