using Zenject;

namespace _UpgradePractice.Scripts
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ConverterInstaller>().FromComponentInHierarchy().AsSingle();
        }
    }
}
