namespace _CardGame.Systems
{
    public sealed class DefaultAttackSystem : IAttackSystem
    {
        private readonly AttackData _damageData;

        public DefaultAttackSystem(AttackData damageData)
        {
            _damageData = damageData;
        }

        public void DealDamage(IHealthSystem targetHealth)
        {
            targetHealth.TakeDamage(_damageData.Damage);
        }
    }
}
