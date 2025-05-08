using System;
using UnityEngine;

public sealed class KeyboardInput : MonoBehaviour
{
    public event Action<Vector3> OnMoveInputChanged;

    private Vector3 _moveDirection = Vector3.zero;


    public void Update()
    {
        HandleMoveInput();
    }

    private void HandleMoveInput()
    {
        _moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _moveDirection += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _moveDirection += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _moveDirection += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveDirection += Vector3.right;
        }

        if (_moveDirection != Vector3.zero)
        {
            _moveDirection.Normalize();
        }

        OnMoveInputChanged?.Invoke(_moveDirection);
    }
}
