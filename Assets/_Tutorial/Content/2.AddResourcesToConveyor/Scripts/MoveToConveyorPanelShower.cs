using System;
using MyCodeBase.UI;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class MoveToConveyorPanelShower : InfoPanelShower
    {
        private AddResourcesToConveyorConfig _config;

        public void Init(AddResourcesToConveyorConfig config)
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
