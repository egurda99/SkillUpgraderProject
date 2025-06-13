namespace _CardGame.Systems
{
    public interface IAttackSystem
    {
        void DealDamage(IHealthSystem targetHealth);
    }
}