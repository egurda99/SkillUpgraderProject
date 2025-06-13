using System;
using _CardGame.Installers;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CardGame
{
    public sealed class DamnOrkAbility : DefaultAbility
    {
        private readonly UIService _uiService;
        private readonly int _mistakeProbability;

        public DamnOrkAbility(UIService uiService, int mistakeProbability)
        {
            _uiService = uiService;
            _mistakeProbability = mistakeProbability;
        }

        public override void OnAttack(HeroView currentHero, HeroView target)
        {
            var currentHeroAttackSystem = currentHero.gameObject.GetComponent<CardInstallerBase>().AttackSystem;
            var isMistake = Random.Range(0, 101);

            var targetHealthSystem = target.gameObject.GetComponent<CardInstallerBase>().HealthSystem;

            if (currentHeroAttackSystem == null)
            {
                throw new Exception("Dont have valid component");
            }

            if (isMistake <= _mistakeProbability) // mistake
            {
                HeroListView enemyList = null;
                if (_uiService.GetBluePlayerList().Contains(currentHero))
                {
                    enemyList = _uiService.GetRedPlayerList();
                }

                else
                {
                    enemyList = _uiService.GetBluePlayerList();
                }

                if (enemyList == null)
                    return;

                var list = enemyList.GetViews();
                if (list.Count == 0)
                    return;

                var newTarget = list[Random.Range(0, list.Count)];
                targetHealthSystem = newTarget.GetComponent<CardInstallerBase>().HealthSystem;

                Debug.Log("Ork made mistake and attacked another card");
            }

            else // no mistake
            {
                Debug.Log("Ork attacked");
                if (currentHeroAttackSystem == null || targetHealthSystem == null)
                {
                    throw new Exception("Dont have valid components");
                }
            }

            currentHeroAttackSystem.DealDamage(targetHealthSystem);
        }
    }
}
