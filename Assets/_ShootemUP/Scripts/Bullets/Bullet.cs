using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour,
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener
    {
        [SerializeField] private BulletConfig _playerBulletConfig;
        [SerializeField] private BulletConfig _enemyBulletConfig;

        private BulletConfig _activeBulletConfig;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private bool _isPlayer;
        private int _damage;
        private Vector2 _activeVelocity;

        public bool IsPlayer => _isPlayer;
        public int Damage => _damage;

        public event Action<Collision2D> OnCollisionEntered;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(collision);
        }

        void IGamePauseListener.OnPauseGame()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }

        void IGameResumeListener.OnResumeGame()
        {
            _rigidbody2D.linearVelocity = _activeVelocity;
        }

        void IGameFinishListener.OnFinishGame()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }


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
            _activeVelocity = velocity;
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

        public sealed class Pool : MonoMemoryPool<Vector2, bool, Vector2, Bullet>
        {
            public event Action<Bullet> OnBulletSpawned;
            public event Action<Bullet> OnBulletDespawned;

            [Inject]
            public void Construct(DiContainer container)
            {
                _container = container;
            }

            protected override void Reinitialize(Vector2 spawnPosition, bool isPlayer, Vector2 direction, Bullet bullet)
            {
                base.Reinitialize(spawnPosition, isPlayer, direction, bullet);
                bullet.Init(spawnPosition, direction, isPlayer);
            }

            protected override void OnCreated(Bullet bullet)
            {
                base.OnCreated(bullet);

                var bulletDamageController = new BulletDamageController(bullet, this);
            }

            protected override void OnSpawned(Bullet bullet)
            {
                base.OnSpawned(bullet);
                OnBulletSpawned?.Invoke(bullet);
            }

            protected override void OnDespawned(Bullet bullet)
            {
                base.OnDespawned(bullet);
                OnBulletDespawned?.Invoke(bullet);
            }
        }
    }
}
