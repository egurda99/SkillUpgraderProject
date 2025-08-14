using _Tutorial.Content;
using _UpgradePractice.Scripts;
using Game.Tutorial.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Tutorial.DI
{
    public sealed class TutorialInstaller : MonoInstaller<TutorialInstaller>
    {
        [SerializeField] private TutorialList _stepList;
        [SerializeField] private GameObject _navigationArrowPrefab;
        [SerializeField] private GameObject _zoneViewPrefab;
        [SerializeField] private Transform _worldTransform;


        public override void InstallBindings()
        {
            Container.Bind<TutorialManager>().AsSingle().WithArguments(_stepList);

            Container.Bind<UpgradeTriggerPoint>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<NavigationManager>().AsSingle()
                .WithArguments(_navigationArrowPrefab, _worldTransform);

            Container.BindInterfacesAndSelfTo<VisualZoneManager>().AsSingle()
                .WithArguments(_zoneViewPrefab, _worldTransform);

            Container.BindInterfacesAndSelfTo<TutorialCompleteObserver>().AsSingle();
        }
    }
}
