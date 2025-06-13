using _CardGame.Installers;
using UI;
using UnityEngine;

namespace _CardGame
{
    public sealed class DevourerAbility : DefaultAbility
    {
        private readonly UIService _uiService;
        private readonly float _damage;

        public DevourerAbility(UIService uiService, float damage)
        {
            _uiService = uiService;
            _damage = damage;
        }

        public override void OnTurnEnd(HeroView hero, HeroView target)
        {
            HeroListView enemyList = null;
            if (_uiService.GetBluePlayerList().Contains(hero))
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
            var targetHealth = newTarget.GetComponent<CardInstallerBase>().HealthSystem;
            targetHealth.TakeDamage(_damage);

            Debug.Log($"<color=red>Devourer damaged {newTarget.gameObject.name} for {_damage}</color>");
        }
    }
}
