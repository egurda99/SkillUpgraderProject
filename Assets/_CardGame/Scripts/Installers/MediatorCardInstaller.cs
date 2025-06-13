using UI;
using UnityEngine;
using Zenject;

namespace _CardGame.Installers
{
    public sealed class MediatorCardInstaller : CardInstallerBase
    {
        [SerializeField] private int _healAmount;


        private ICardAbility _ability;
        private UIService _uiService;

        public override ICardAbility CardAbility => _ability;

        [Inject]
        private void Construct(UIService uiService)
        {
            _uiService = uiService;
        }


        protected override void Awake()
        {
            base.Awake();
            _ability = new MediatorAbility(_healAmount, _uiService);
        }
    }
}