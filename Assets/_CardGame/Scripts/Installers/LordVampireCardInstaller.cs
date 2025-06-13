using UnityEngine;

namespace _CardGame.Installers
{
    public sealed class LordVampireCardInstaller : CardInstallerBase
    {
        [SerializeField] private int _healProbability = 50;


        private ICardAbility _ability;

        public override ICardAbility CardAbility => _ability;


        protected override void Awake()
        {
            base.Awake();
            _ability = new LordVampireAbility(_healProbability);
        }
    }
}
