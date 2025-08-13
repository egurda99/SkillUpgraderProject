using Atomic.Entities;
using MyCodeBase;
using UnityEngine;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class UpgradeTriggerPoint : MonoBehaviour
    {
        private PopupManager _popupManager;


        [Inject]
        public void Construct(PopupManager popupManager)
        {
            _popupManager = popupManager;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SceneEntityProxy entity))
            {
                if (entity.HasPlayerTag())
                {
                    _popupManager.ShowPopup(PopupName.CONVERTER_UPGRADES);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out SceneEntityProxy entity))
            {
                if (entity.HasPlayerTag())
                {
                    _popupManager.HidePopup(PopupName.CONVERTER_UPGRADES);
                }
            }
        }
    }
}
