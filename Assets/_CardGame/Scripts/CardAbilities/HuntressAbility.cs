using UI;
using UnityEngine;

namespace _CardGame
{
    public sealed class HuntressAbility : DefaultAbility
    {
        public override void OnAttacked(HeroView currentHero, HeroView target)
        {
            Debug.Log("<color=red>Huntress defended from counterAttack</color>");
        }
    }
}
