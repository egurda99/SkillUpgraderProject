using UI;

namespace _CardGame
{
    public interface ICardAbility
    {
        void OnAttack(HeroView currentHero, HeroView target);
        void OnAttacked(HeroView currentHero, HeroView target);
        void OnTurnEnd(HeroView currentHero, HeroView target);
    }
}
