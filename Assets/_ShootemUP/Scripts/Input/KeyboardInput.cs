using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class KeyboardInput : MonoBehaviour, IInput
    {
        public event Action<Vector2> OnMoveInputChanged;
        public event Action OnFireClicked;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFireClicked?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                OnMoveInputChanged?.Invoke(Vector2.left);
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                OnMoveInputChanged?.Invoke(Vector2.right);
            }
        }
    }
}
