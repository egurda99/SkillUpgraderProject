namespace _CardGame.Systems
{
    public sealed class AttackSystem : IAttackSystem
    {
        private readonly AttackData _damageData;

        public AttackSystem(AttackData damageData)
        {
            _damageData = damageData;
        }

        public void DealDamage(IHealthSystem targetHealth)
        {
            targetHealth.TakeDamage(_damageData.Damage);
        }
    }
}
