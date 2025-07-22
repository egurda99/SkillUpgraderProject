using UnityEngine;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class TimeConvertationUpgrade : Upgrade
    {
        private readonly TimeConvertationUpgradeConfig _timeConvertationUpgradeConfig;
        private ConverterData _converterData;

        public TimeConvertationUpgrade(TimeConvertationUpgradeConfig config) : base(config)
        {
            _timeConvertationUpgradeConfig = config;
        }

        [Inject]
        public void Construct(ConverterInstaller converterInstaller)
        {
            _converterData = converterInstaller.View.Data;
            var time = _timeConvertationUpgradeConfig.TimeConvertationTable.GetTime(Level);
            _converterData.SetConvertationTime(time);
        }

        protected override void OnUpgrade()
        {
            var time = _timeConvertationUpgradeConfig.TimeConvertationTable.GetTime(Level);
            _converterData.SetConvertationTime(time);
            Debug.Log($"<color=blue>Convertation time Upgraded. New time: {time}</color>");
        }
    }
}
