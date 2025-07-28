namespace ShootEmUp
{
    public sealed class EnemyObserversCreator
    {
        private readonly HealthComponent _healthComponent;
        private readonly Enemy.Pool _enemyPool;

        private readonly Enemy _enemy;

        private readonly EnemyMoveAgent _moveAgent;
        private readonly EnemyAttackAgent _attackAgent;

        public EnemyObserversCreator(HealthComponent healthComponent, Enemy.Pool enemyPool, Enemy enemy,
            EnemyMoveAgent moveAgent, EnemyAttackAgent attackAgent)
        {
            _healthComponent = healthComponent;
            _enemyPool = enemyPool;
            _enemy = enemy;
            _moveAgent = moveAgent;
            _attackAgent = attackAgent;
        }

        public void CreateObservers()
        {
            CreateEnemyDeathObserver();
            CreateEnemyPositionObserver();
        }

        private void CreateEnemyPositionObserver()
        {
            var enemyPositionObserver = new EnemyPositionObserver(_moveAgent, _attackAgent);
        }

        private void CreateEnemyDeathObserver()
        {
            var deathObserver = new EnemyDeathObserver(_healthComponent, _enemyPool, _enemy);
        }
    }
}