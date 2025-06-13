using _CardGame.Systems;
using UI;
using UnityEngine;
using Zenject;

namespace _CardGame.Installers
{
    public sealed class ElectroMageCardInstaller : CardInstallerBase
    {
        [SerializeField] private int _electroDamageAmount = 1;


        private ICardAbility _ability;
        private ElectroAttackMechanic _electroAttack;
        private UIService _uiService;

        public override ICardAbility CardAbility => _ability;


        [Inject]
        public void Construct(UIService uiService)
        {
            _uiService = uiService;
        }

        protected override void Awake()
        {
            base.Awake();
            _ability = new DefaultAbility();
            _electroAttack = new ElectroAttackMechanic(_healthSystem, _uiService, _heroView, _electroDamageAmount);
        }

        public override void Dispose()
        {
            base.Dispose();
            _electroAttack.Dispose();
        }
    }
}