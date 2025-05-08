using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    public event Action<float> OnMouseDeltaXChanged;
    public event Action OnFireClicked;


    public event Action<Vector3> OnMouseWorldPositionChanged;


    private void Update()
    {
        HandleRotate();
        HandleLMBClick();
    }

    private void HandleRotate()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;

        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, 100f, LayerMask.GetMask("Ground")))
        {
            OnMouseWorldPositionChanged?.Invoke(hit.point);
        }
    }

    private void HandleLMBClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnFireClicked?.Invoke();
        }
    }
}
