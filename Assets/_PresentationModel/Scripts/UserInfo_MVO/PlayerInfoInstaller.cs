using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerInfoInstaller : MonoInstaller<PlayerInfoInstaller>
    {
        public override void InstallBindings()
        {
            var playerInfo = FindObjectOfType<PlayerInfoInstallerHelper>();

            Container.Bind<UserInfo>()
                .AsSingle()
                .WithArguments(playerInfo.Name, playerInfo.Description, playerInfo.IconSprite);

            Container.BindInterfacesAndSelfTo<UserInfoAdapter>()
                .AsSingle()
                .WithArguments(playerInfo.UserInfoView);
        }
    }
}
