using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class PlayerInputController : IContextInit, IContextDispose
{
    private readonly SceneEntity _sceneEntity;

    private KeyboardInput _keyboardInput;

    private MouseInput _mouseInput;
    private ReactiveVariable<Vector3> _moveDirection;

    private ReactiveVariable<Vector3> _mouseTargetPosition;


    public PlayerInputController(SceneEntity sceneEntity)
    {
        _sceneEntity = sceneEntity;
    }

    public void Init(Atomic.Contexts.IContext context)
    {
        _keyboardInput = context.GetKeyboardInput();
        _mouseInput = context.GetMouseInput();

        _mouseTargetPosition = _sceneEntity.GetTargetPosition();


        _moveDirection = _sceneEntity.GetMoveDirection();

        _keyboardInput.OnMoveInputChanged += OnMoveInputChanged;
        _mouseInput.OnMouseWorldPositionChanged += OnMouseWorldPositionChanged;
    }

    private void OnMouseWorldPositionChanged(Vector3 direction)
    {
        _mouseTargetPosition.Value = direction;
    }


    private void OnMoveInputChanged(Vector3 direction)
    {
        _moveDirection.Value = direction;
    }

    public void Dispose(Atomic.Contexts.IContext context)
    {
        _keyboardInput.OnMoveInputChanged -= OnMoveInputChanged;
        _mouseInput.OnMouseWorldPositionChanged -= OnMouseWorldPositionChanged;
    }
}
