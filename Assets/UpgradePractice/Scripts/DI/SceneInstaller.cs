using Zenject;

namespace _UpgradePractice.Scripts
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ConverterInstaller>().FromComponentInHierarchy().AsSingle();
            BindMoneyStorage();
            //   BindUpgradeManager();
        }

        private void BindMoneyStorage()
        {
            Container.Bind<MoneyStorage>().AsSingle();
        }

        private void BindUpgradeManager()
        {
            var helper = FindObjectOfType<SceneInstallerHelper>();

            Container.Bind<UpgradesManager>().AsSingle().WithArguments(helper.UpgradeCatalog);
        }
    }
}
