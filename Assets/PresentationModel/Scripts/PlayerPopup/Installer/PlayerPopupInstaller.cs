using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupInstaller : MonoInstaller<PlayerPopupInstaller>
    {
        public override void InstallBindings()
        {
            var userInfoHelper = FindObjectOfType<UserInfoInstallerHelper>();
            var playerPopupHelper = FindObjectOfType<PlayerPopupInstallerHelper>();
            var statsInstallerHelper = FindObjectOfType<StatsInstallerHelper>();

            BindStatsSection(statsInstallerHelper);

            BindPlayerLevelSection();

            BindUserInfoSection(userInfoHelper);


            BindPlayerPopup(userInfoHelper, playerPopupHelper);
        }

        private void BindPlayerPopup(UserInfoInstallerHelper userInfoHelper,
            PlayerPopupInstallerHelper playerPopupHelper)
        {
            Container.Bind<PlayerPopupSectionsFactory>()
                .AsSingle()
                .WithArguments(userInfoHelper.UserInfoView, playerPopupHelper.PlayerLevelView);
        }

        private void BindUserInfoSection(UserInfoInstallerHelper userInfoHelper)
        {
            Container.Bind<UserInfo>()
                .AsSingle()
                .WithArguments(userInfoHelper.Name, userInfoHelper.Description, userInfoHelper.IconSprite);
        }

        private void BindPlayerLevelSection()
        {
            Container.Bind<PlayerLevel>().AsSingle();
        }

        private void BindStatsSection(StatsInstallerHelper statsInstallerHelper)
        {
            Container.Bind<StatViewFactory>()
                .AsSingle()
                .WithArguments(statsInstallerHelper.StatContainer, statsInstallerHelper.StatPrefab);

            Container.Bind<StatAdapterFactory>().AsSingle();
            Container.Bind<StatsListHandlerFactory>().AsSingle();
            Container.Bind<CharacterStatsHolder>().AsSingle();
        }
    }
}
