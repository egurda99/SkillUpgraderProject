using Game.Tutorial.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Tutorial.DI
{
    public sealed class TutorialInstaller : MonoInstaller<TutorialInstaller>
    {
        [SerializeField] private TutorialList _stepList;
        [SerializeField] private NavigationArrow _navigationArrow;
        [SerializeField] private GameObject _pointerGO;
        [SerializeField] private Transform _worldTransform;


        public override void InstallBindings()
        {
            Container.Bind<TutorialManager>().AsSingle().WithArguments(_stepList);
            Container.Bind<NavigationManager>().AsSingle().WithArguments(_navigationArrow, _worldTransform);
            Container.Bind<PointerManager>().AsSingle().WithArguments(_pointerGO);
        }
    }
}
