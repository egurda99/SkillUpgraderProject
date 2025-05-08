using System;
using UnityEngine;

public sealed class CameraBasedKeyboardInput : MonoBehaviour
{
    public event Action<Vector3> OnMoveInputChanged;

    [SerializeField] private Transform _cameraTransform;

    private Vector3 _moveDirection = Vector3.zero;

    private void Update()
    {
        HandleMoveInput();
    }

    private void HandleMoveInput()
    {
        Vector3 input = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) input += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) input += Vector3.back;
        if (Input.GetKey(KeyCode.A)) input += Vector3.left;
        if (Input.GetKey(KeyCode.D)) input += Vector3.right;

        if (input.sqrMagnitude > 0)
        {
            input.Normalize();

            // Получаем forward и right камеры, но убираем Y, чтобы не было движения вверх
            Vector3 cameraForward = _cameraTransform.forward;
            Vector3 cameraRight = _cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            // Переводим локальный ввод в мировое направление
            _moveDirection = cameraForward * input.z + cameraRight * input.x;
        }
        else
        {
            _moveDirection = Vector3.zero;
        }

        OnMoveInputChanged?.Invoke(_moveDirection);
    }
}