using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyMoveAgent))]
    [RequireComponent(typeof(EnemyAttackAgent))]
    public sealed class EnemyPositionObserver : MonoBehaviour
    {
        private EnemyMoveAgent _moveAgent;
        private EnemyAttackAgent _attackAgent;

        private void Awake()
        {
            _moveAgent = GetComponent<EnemyMoveAgent>();
            _attackAgent = GetComponent<EnemyAttackAgent>();
        }

        private void OnEnable()
        {
            _moveAgent.OnPositionReached += OnEnemyReachPosition;
        }

        private void OnEnemyReachPosition()
        {
            _attackAgent.SetPositionReached();
        }
    }
}