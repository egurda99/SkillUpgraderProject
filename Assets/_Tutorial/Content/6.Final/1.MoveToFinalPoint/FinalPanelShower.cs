using System;
using MyCodeBase.UI;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class FinalPanelShower : InfoPanelShower
    {
        private FinalStepConfig _config;

        public void Init(FinalStepConfig config)
        {
            _config = config;
        }

        protected override void OnShow()
        {
            view.SetTitle(_config.Title);
            view.SetDescription(_config.Description);
            view.SetIcon(_config.Sprite);
        }
    }
}