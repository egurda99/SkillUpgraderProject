using System;
using ShootemUP;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class KeyboardInput : MonoBehaviour,
        IInput,
        IGameUpdateListener
    {
        private bool _isActive;
        public event Action<Vector2> OnMoveInputChanged;
        public event Action OnFireClicked;

        void IGameUpdateListener.OnUpdate(float deltaTime)
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
