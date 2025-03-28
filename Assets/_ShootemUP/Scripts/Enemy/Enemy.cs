using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyMoveAgent))]
    [RequireComponent(typeof(EnemyAttackAgent))]
    public sealed class Enemy : MonoBehaviour
    {
        private EnemyMoveAgent _enemyMoveAgent;
        private EnemyAttackAgent _enemyAttackAgent;

        private void Awake()
        {
            _enemyAttackAgent = GetComponent<EnemyAttackAgent>();
            _enemyMoveAgent = GetComponent<EnemyMoveAgent>();
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
            public event Action<Enemy> EnemyCreated;
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

                var enemyDeathObserver = new EnemyDeathObserver(enemy.GetComponent<HealthComponent>(), this, enemy);

                EnemyCreated?.Invoke(enemy);
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
