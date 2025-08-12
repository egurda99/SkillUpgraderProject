using Game.Tutorial.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Tutorial.DI
{
    public sealed class TutorialInstaller : MonoInstaller<TutorialInstaller>
    {
        [SerializeField] private TutorialList _stepList;
        [SerializeField] private GameObject _navigationArrowPrefab;
        [SerializeField] private GameObject _pointerGO;
        [SerializeField] private Transform _worldTransform;


        public override void InstallBindings()
        {
            Container.Bind<TutorialManager>().AsSingle().WithArguments(_stepList);
            Container.BindInterfacesAndSelfTo<NavigationManager>().AsSingle()
                .WithArguments(_navigationArrowPrefab, _worldTransform);
            Container.Bind<PointerManager>().AsSingle().WithArguments(_pointerGO);
        }
    }
}
