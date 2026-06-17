using Lessons.Architecture.PM;
using UnityEngine;
using Zenject;

namespace Modules.Popups
{
    public sealed class PopupZenjectInstaller : MonoInstaller
    {
        [SerializeField] private PopupCatalog _catalog;
        [SerializeField] private PopupManagerZenject _popupManager;

        public override void InstallBindings()
        {
            Container.Bind<PlayerLevel>().AsSingle();
            Container.Bind<PopupCatalog>().FromInstance(_catalog).AsSingle();
            Container.Bind<PopupManagerZenject>().FromInstance(_popupManager).AsSingle();
        }
    }
}
