using ShootemUP;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyMoveAgent))]
    [RequireComponent(typeof(EnemyAttackAgent))]
    public sealed class EnemyPositionObserver : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener
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

        private void OnDisable()
        {
            _moveAgent.OnPositionReached -= OnEnemyReachPosition;
        }

        void IGameStartListener.OnStartGame()
        {
            // Debug.Log("<color=blue>StartObserver</color>");
            // _moveAgent.OnPositionReached += OnEnemyReachPosition;
        }

        void IGameFinishListener.OnFinishGame()
        {
            // _moveAgent.OnPositionReached -= OnEnemyReachPosition;
        }

        private void OnEnemyReachPosition()
        {
            Debug.Log("<color=blue>ReachedObserver</color>");
            _attackAgent.SetPositionReached();
        }
    }
}
