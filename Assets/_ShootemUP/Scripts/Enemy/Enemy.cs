using UnityEngine;

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
    }
}
