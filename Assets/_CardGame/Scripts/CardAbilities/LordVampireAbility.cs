using _CardGame.Installers;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CardGame
{
    public sealed class LordVampireAbility : DefaultAbility
    {
        private readonly int _healProbability;

        public LordVampireAbility(int healProbability)
        {
            _healProbability = healProbability;
        }

        public override void OnTurnEnd(HeroView hero, HeroView target)
        {
            var isHealActive = Random.Range(0, 101);


            if (isHealActive <= _healProbability) // heal
            {
                var currentHeroHealthSystem = hero.gameObject.GetComponent<CardInstallerBase>().HealthSystem;
                var currentHeroAttackData = hero.gameObject.GetComponent<CardInstallerBase>().CardView.AttackData;

                currentHeroHealthSystem.Heal(currentHeroAttackData.Damage);
                Debug.Log("Vampire healed on : " + currentHeroAttackData.Damage);
            }
            else
            {
                Debug.Log("Vampire didnt heal");
            }
        }
    }
}
