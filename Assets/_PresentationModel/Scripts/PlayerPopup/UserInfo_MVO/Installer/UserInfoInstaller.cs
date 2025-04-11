using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfoInstaller : MonoInstaller<UserInfoInstaller>
    {
        public override void InstallBindings()
        {
            var playerInfo = FindObjectOfType<UserInfoInstallerHelper>();

            Container.Bind<UserInfo>()
                .AsSingle()
                .WithArguments(playerInfo.Name, playerInfo.Description, playerInfo.IconSprite);

            Container.BindInterfacesAndSelfTo<UserInfoAdapter>()
                .AsSingle()
                .WithArguments(playerInfo.UserInfoView);
        }
    }
}
