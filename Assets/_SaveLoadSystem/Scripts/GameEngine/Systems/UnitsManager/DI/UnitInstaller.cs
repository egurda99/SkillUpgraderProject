using Zenject;

namespace GameEngine
{
    public sealed class UnitInstaller : MonoInstaller<UnitInstaller>
    {
        public override void InstallBindings()
        {
            var helper = FindAnyObjectByType<UnitInstallerHelper>();

            Container.BindInterfacesAndSelfTo<UnitManager>()
                .AsSingle()
                .WithArguments(helper.UnitContainer);

            Container.Bind<UnitSpawner>()
                .AsSingle()
                .WithArguments(helper.OrcMountedShamanPrefab, helper.OrcArcherPrefab, helper.WKWorkerPrefab,
                    helper.WKCatapultPrefab, helper.WKSpearmanAPrefab, helper.UnitContainer);
        }
    }
}
