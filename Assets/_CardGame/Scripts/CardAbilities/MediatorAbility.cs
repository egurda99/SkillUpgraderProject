using _CardGame.Installers;
using UI;
using UnityEngine;

namespace _CardGame
{
    public sealed class MediatorAbility : DefaultAbility
    {
        private readonly int _healAmount;
        private readonly UIService _uiService;

        public MediatorAbility(int healAmount, UIService uiService)
        {
            _healAmount = healAmount;
            _uiService = uiService;
        }

        public override void OnTurnEnd(HeroView hero, HeroView target)
        {
            HeroListView friendTeamList = null;
            if (_uiService.GetBluePlayerList().Contains(hero))
            {
                friendTeamList = _uiService.GetBluePlayerList();
            }

            else
            {
                friendTeamList = _uiService.GetRedPlayerList();
            }

            if (friendTeamList == null)
                return;

            var list = friendTeamList.GetViews();
            if (list.Count == 0)
                return;

            var healTarget = list[Random.Range(0, list.Count)];
            var targetHealth = healTarget.GetComponent<CardInstallerBase>().HealthSystem;
            targetHealth.Heal(_healAmount);

            Debug.Log($"<color=red>Mediator healed {healTarget.gameObject.name} for {_healAmount}</color>");
        }
    }
}
