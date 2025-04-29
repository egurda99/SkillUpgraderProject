using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupInstaller : MonoInstaller<PlayerPopupInstaller>
    {
        public override void InstallBindings()
        {
            var helper = FindObjectOfType<PlayerPopupInstallerHelper>();

            Container.Bind<PlayerPopupViewModelFactory>()
                .AsSingle()
                .WithArguments(helper.PlayerPopupView);
        }
    }
}
