using Zenject;

namespace GameEngine
{
    public sealed class UnitInstaller : MonoInstaller<UnitInstaller>
    {
        public override void InstallBindings()
        {
            var helper = FindAnyObjectByType<UnitInstallerHelper>();

            Container.Bind<UnitManager>().AsSingle();

            Container.Bind<UnitPrefabProvider>()
                .AsSingle()
                .WithArguments(helper.OrcMountedShamanPrefab,
                    helper.OrcArcherPrefab,
                    helper.WKWorkerPrefab,
                    helper.WKCatapultPrefab,
                    helper.WKSpearmanAPrefab);

            Container.Bind<UnitSpawner>()
                .AsSingle()
                .WithArguments(helper.UnitContainer);
        }
    }
}
