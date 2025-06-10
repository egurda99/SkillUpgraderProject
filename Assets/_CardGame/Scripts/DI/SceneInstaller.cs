using _CardGame.Pipeline;
using _CardGame.Services;
using UI;
using Zenject;

namespace _CardGame.DI
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            BindPipelines();

            BindUI();


            Container.Bind<ActiveTeamService>().AsSingle();
            Container.Bind<ActiveCardService>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<UIService>().FromComponentInHierarchy().AsSingle();
        }

        private void BindPipelines()
        {
            Container.Bind<TurnPipeline>().FromNew().AsSingle().NonLazy();
            Container.Bind<VisualPipeline>().FromNew().AsSingle().NonLazy();
        }
    }
}
