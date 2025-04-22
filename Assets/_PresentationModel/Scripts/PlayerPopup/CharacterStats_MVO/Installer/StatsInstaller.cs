using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class StatsInstaller : MonoInstaller<StatsInstaller>
    {
        public override void InstallBindings()
        {
            var helper = FindObjectOfType<StatsInstallerHelper>();

            Container.Bind<StatViewFactory>()
                .AsSingle()
                .WithArguments(helper.StatContainer, helper.StatPrefab);

            Container.Bind<StatAdapterFactory>().AsSingle();

            Container.Bind<StatsListView>().AsSingle();

            Container.BindInterfacesAndSelfTo<StatListView>().AsSingle();

            Container.Bind<CharacterStatsHolder>().AsSingle();
        }
    }
}
