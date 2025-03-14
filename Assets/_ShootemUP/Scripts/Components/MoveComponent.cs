using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;

        private Rigidbody2D _rigidbody2D;

        private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}
