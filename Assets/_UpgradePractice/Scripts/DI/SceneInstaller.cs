using Zenject;

namespace _UpgradePractice.Scripts
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        private SceneInstallerHelper _helper;

        public override void InstallBindings()
        {
            _helper = FindObjectOfType<SceneInstallerHelper>();

            BindConverterDataService();
            BindMoneyStorage();
            BindUpgradeManager();
        }

        private void BindConverterDataService()
        {
            var converterData = new ConverterData(_helper.ResourceExchangeRates);

            Container.Bind<ConverterDataService>().AsSingle().WithArguments(converterData);
        }

        private void BindMoneyStorage()
        {
            Container.Bind<MoneyStorage>().AsSingle();
        }

        private void BindUpgradeManager()
        {
            Container.Bind<UpgradesManager>().AsSingle().WithArguments(_helper.UpgradeCatalog);
        }
    }
}
