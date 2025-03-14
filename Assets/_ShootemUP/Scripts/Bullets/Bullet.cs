using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletConfig _playerBulletConfig;
        [SerializeField] private BulletConfig _enemyBulletConfig;

        private BulletConfig _activeBulletConfig;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private bool _isPlayer;
        private int _damage;

        public bool IsPlayer => _isPlayer;
        public int Damage => _damage;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(collision);
        }

        public event Action<Collision2D> OnCollisionEntered;

        public void Init(Vector2 position, Vector2 direction, bool isPlayer)
        {
            SetConfig(isPlayer);
            SetTeam(isPlayer);
            SetPosition(position);
            SetVelocity(_activeBulletConfig.Speed * direction);
            SetPhysicsLayer(_activeBulletConfig.PhysicsLayer);
            SetColor(_activeBulletConfig.Color);
            SetDamage(_activeBulletConfig.Damage);
        }

        private void SetTeam(bool isPlayer)
        {
            _isPlayer = isPlayer;
        }

        private void SetDamage(int damage)
        {
            _damage = damage;
        }

        private void SetConfig(bool isPlayer)
        {
            _activeBulletConfig = isPlayer ? _playerBulletConfig : _enemyBulletConfig;
        }

        private void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.linearVelocity = velocity;
        }

        private void SetPhysicsLayer(PhysicsLayer physicsLayer)
        {
            gameObject.layer = (int)physicsLayer;
        }

        private void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        private void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}
