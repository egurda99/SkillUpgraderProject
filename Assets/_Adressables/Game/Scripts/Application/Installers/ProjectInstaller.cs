using AssetManager;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "ProjectInstaller",
        menuName = "Installers/New ProjectInstaller"
    )]
    public sealed class ProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AddressableAssetManager>().AsSingle().NonLazy();
            Container.Bind<ApplicationExiter>().AsSingle().NonLazy();
            Container.Bind<GameLoader>().AsSingle().NonLazy();
            Container.Bind<MenuLoader>().AsSingle().NonLazy();
        }
    }
}
