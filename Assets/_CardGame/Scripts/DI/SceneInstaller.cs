using System;
using _CardGame.Controllers;
using _CardGame.EventTasks;
using _CardGame.Installers;
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
            BindServices();

            BindPipelines();
            BindTasks();


            BindEventBus();
            BindControllers();

            BindCardInstallers();
        }


        private void BindCardInstallers()
        {
            var cardInstallers = FindObjectsOfType<CardInstaller>(true);

            foreach (var cardInstaller in cardInstallers)
            {
                Container
                    .Bind<IDisposable>()
                    .FromInstance(cardInstaller)
                    .AsTransient();
            }
        }

        private void BindTasks()
        {
            Container.Bind<ChooseActiveTeamTask>().FromNew().AsSingle();
            Container.Bind<ChooseActiveHeroTask>().FromNew().AsSingle();
            Container.Bind<WaitForChooseTargetTask>().FromNew().AsSingle();

            Container.Bind<AttackVisualTask>().FromNew().AsSingle();
        }

        private void BindEventBus()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<HeroesActivationStatusController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HeroAttackController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HeroesDeathCheckController>().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<UIService>().FromComponentInHierarchy().AsSingle();

            Container.Bind<ActiveTeamService>().AsSingle().NonLazy();
            Container.Bind<ActiveCardService>().AsSingle();
        }

        private void BindPipelines()
        {
            Container.Bind<TurnPipeline>().FromNew().AsSingle().NonLazy();
            Container.Bind<VisualPipeline>().FromNew().AsSingle().NonLazy();
        }
    }
}
