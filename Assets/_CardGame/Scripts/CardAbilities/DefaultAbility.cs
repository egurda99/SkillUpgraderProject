using System;
using _CardGame.Installers;
using UI;
using UnityEngine;

namespace _CardGame
{
    public class DefaultAbility : ICardAbility
    {
        public virtual void OnAttack(HeroView currentHero, HeroView target)
        {
            var currentHeroAttackSystem = currentHero.gameObject.GetComponent<CardInstallerBase>().AttackSystem;

            var targetHealthSystem = target.gameObject.GetComponent<CardInstallerBase>().HealthSystem;

            if (currentHeroAttackSystem == null || targetHealthSystem == null)
            {
                throw new Exception("Dont have valid components");
            }

            currentHeroAttackSystem.DealDamage(targetHealthSystem);
        }

        public virtual void OnAttacked(HeroView currentHero, HeroView target)
        {
            var currentHeroAttackSystem = currentHero.gameObject.GetComponent<CardInstallerBase>().AttackSystem;
            var currentHeroHealthSystem = currentHero.gameObject.GetComponent<CardInstallerBase>().HealthSystem;

            var targetHealthSystem = target.gameObject.GetComponent<CardInstallerBase>().HealthSystem;
            var targetAttackSystem = target.gameObject.GetComponent<CardInstallerBase>().AttackSystem;

            if (currentHeroAttackSystem == null || targetHealthSystem == null)
            {
                throw new Exception("Dont have valid components");
            }

            targetAttackSystem.DealDamage(currentHeroHealthSystem);
        }

        public virtual void OnTurnEnd(HeroView hero, HeroView target)
        {
            Debug.Log("OnTurnEnd");
        }
    }
}
