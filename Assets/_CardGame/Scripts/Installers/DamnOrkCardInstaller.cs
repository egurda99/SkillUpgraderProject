using UI;
using UnityEngine;
using Zenject;

namespace _CardGame.Installers
{
    public sealed class DamnOrkCardInstaller : CardInstallerBase
    {
        [SerializeField] [Range(0, 100)] private int _mistakeProbability;


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
            _ability = new DamnOrkAbility(_uiService, _mistakeProbability);
        }
    }
}
