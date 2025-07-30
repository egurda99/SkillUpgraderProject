namespace MyCodeBase.HybridComponents
{
    public sealed class HealthSystem
    {
        private readonly HealthData _healthData;

        public HealthSystem(HealthData healthData)
        {
            _healthData = healthData;
        }

        public void TakeDamage(float damage)
        {
            if (!_healthData.IsAlive)
                return;

            _healthData.TakeDamage(damage);
        }

        public void Heal(float heal)
        {
            if (!_healthData.IsAlive)
                return;

            _healthData.AddHealth(heal);
        }
    }
}
