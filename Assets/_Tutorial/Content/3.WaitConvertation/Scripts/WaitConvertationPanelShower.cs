using System;
using MyCodeBase.UI;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class WaitConvertationPanelShower : InfoPanelShower
    {
        private WaitConvertationConfig _config;

        public void Init(WaitConvertationConfig config)
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
