using MyCodeBase.UI;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ConverterInstaller>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<DoTweenAnimationManager>().AsSingle().NonLazy();
            BindMoneyStorage();
            BindUpgradeManager();
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
