using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyAttackAgent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(TeamComponent))]
    [RequireComponent(typeof(ShootComponent))]
    public sealed class Enemy : MonoBehaviour, IGameFixedUpdateListener
    {
        private EnemyMoveAgent _enemyMoveAgent;
        private EnemyAttackAgent _enemyAttackAgent;
        private Pool _pool;

        public EnemyMoveAgent EnemyMoveAgent => _enemyMoveAgent;

        [Inject]
        public void Construct(Pool pool)
        {
            _pool = pool;
        }

        private void Awake()
        {
            _enemyMoveAgent = new EnemyMoveAgent(GetComponent<MoveComponent>(), transform);
            _enemyAttackAgent = GetComponent<EnemyAttackAgent>();

            var enemyDeathObserver = new EnemyDeathObserver(GetComponent<HealthComponent>(), _pool, this);
            var enemyPositionObserver = new EnemyPositionObserver(_enemyMoveAgent, _enemyAttackAgent);
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            _enemyMoveAgent.OnFixedUpdate(fixedDeltaTime);
        }

        public void Init(Transform enemyTarget, Vector2 destination, Vector2 startPosition)
        {
            transform.position = startPosition;
            _enemyAttackAgent.SetTarget(enemyTarget);
            _enemyMoveAgent.SetDestination(destination);
        }

        public sealed class Pool : MonoMemoryPool<Transform, Vector2, Vector2, Enemy>
        {
            public event Action<Enemy> EnemySpawned;
            public event Action<Enemy> EnemyDespawned;


            protected override void Reinitialize(Transform enemyTarget, Vector2 destination, Vector2 startPosition,
                Enemy enemy)
            {
                base.Reinitialize(enemyTarget, destination, startPosition, enemy);
                enemy.Init(enemyTarget, destination, startPosition);
            }

            protected override void OnCreated(Enemy enemy)
            {
                base.OnCreated(enemy);

                // var enemyDeathObserver = new EnemyDeathObserver(enemy.GetComponent<HealthComponent>(), this, enemy);
                // var enemyPositionObserver = new EnemyPositionObserver(enemy.EnemyMoveAgent,
                //     enemy.GetComponent<EnemyAttackAgent>());
            }


            protected override void OnSpawned(Enemy enemy)
            {
                base.OnSpawned(enemy);

                EnemySpawned?.Invoke(enemy);
            }

            protected override void OnDespawned(Enemy enemy)
            {
                base.OnDespawned(enemy);
                EnemyDespawned?.Invoke(enemy);
            }
        }
    }
}
