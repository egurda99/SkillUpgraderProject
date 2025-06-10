namespace _CardGame.Systems
{
    public sealed class AttackSystem
    {
        private readonly AttackData _damageData;

        public AttackSystem(AttackData damageData)
        {
            _damageData = damageData;
        }

        public void DealDamage(HealthSystem targetHealth)
        {
            targetHealth.TakeDamage(_damageData.Damage);
        }
    }
}