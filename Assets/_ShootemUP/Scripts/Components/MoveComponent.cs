using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : MonoBehaviour,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField] private float _speed = 5.0f;

        private Rigidbody2D _rigidbody2D;
        private bool _isPause;

        private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            if (_isPause)
                return;

            var nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }

        void IGamePauseListener.OnPauseGame()
        {
            _isPause = true;
        }

        void IGameResumeListener.OnResumeGame()
        {
            _isPause = false;
        }
    }
}
