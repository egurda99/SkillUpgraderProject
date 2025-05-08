using System;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class PlayerInputController : IContextInit, IContextDispose
{
    [SerializeField] private SceneEntity _sceneEntity;

    // private KeyboardInput _keyboardInput;

    private CameraBasedKeyboardInput _keyboardInput;
    private MouseInput _mouseInput;
    private ReactiveVariable<Vector3> _moveDirection;

    private IEvent _shootRequest;
    private ReactiveVariable<Vector3> _mouseTargetPosition;

    public void Init(IContext context)
    {
        _keyboardInput = context.GetCameraBasedKeyboardInput();
        // _keyboardInput = context.GetKeyboardInput();
        _mouseInput = context.GetMouseInput();

        _mouseTargetPosition = _sceneEntity.GetTargetPosition();


        _moveDirection = _sceneEntity.GetMoveDirection();
        _shootRequest = _sceneEntity.GetShootRequest();

        _keyboardInput.OnMoveInputChanged += OnMoveInputChanged;
        _mouseInput.OnFireClicked += OnFireClicked;
        _mouseInput.OnMouseWorldPositionChanged += OnMouseWorldPositionChanged;
    }

    private void OnMouseWorldPositionChanged(Vector3 direction)
    {
        _mouseTargetPosition.Value = direction;
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
        _mouseInput.OnFireClicked -= OnFireClicked;
        _mouseInput.OnMouseWorldPositionChanged -= OnMouseWorldPositionChanged;
    }
}
