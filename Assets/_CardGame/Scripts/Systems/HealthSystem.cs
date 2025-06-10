namespace _CardGame.Systems
{
    public sealed class HealthSystem
    {
        private readonly HealthData _healthData;

        public HealthSystem(HealthData data)
        {
            _healthData = data;
        }

        public void TakeDamage(float amount)
        {
            var currentHealth = _healthData.CurrentHealth;
            var newHealth = currentHealth - amount;
            _healthData.SetCurrentHealth(newHealth);
        }

        public void Heal(float amount)
        {
            var currentHealth = _healthData.CurrentHealth;
            var newHealth = currentHealth + amount;
            _healthData.SetCurrentHealth(newHealth);
        }

        public void Kill()
        {
            _healthData.SetCurrentHealth(0);
        }
    }
}
