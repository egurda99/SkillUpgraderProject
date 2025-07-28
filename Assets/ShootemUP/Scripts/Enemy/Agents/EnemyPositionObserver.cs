using System;

namespace ShootEmUp
{
    public sealed class EnemyPositionObserver : IDisposable
    {
        private readonly EnemyMoveAgent _moveAgent;
        private readonly EnemyAttackAgent _attackAgent;

        public EnemyPositionObserver(EnemyMoveAgent moveAgent, EnemyAttackAgent attackAgent)
        {
            _moveAgent = moveAgent;
            _attackAgent = attackAgent;

            _moveAgent.OnPositionReached += OnEnemyReachPosition;
        }

        void IDisposable.Dispose()
        {
            _moveAgent.OnPositionReached -= OnEnemyReachPosition;
        }

        private void OnEnemyReachPosition() => _attackAgent.SetPositionReached();
    }
}
