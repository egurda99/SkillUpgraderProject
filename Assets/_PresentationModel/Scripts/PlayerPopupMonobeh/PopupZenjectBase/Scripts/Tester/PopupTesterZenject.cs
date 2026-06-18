using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Modules.Popups
{
    public sealed class PopupTesterZenject : MonoBehaviour
    {
        [SerializeField] private PopupType _popupType;

        private PopupManagerZenject _popupManager;

        [Inject]
        public void Construct(PopupManagerZenject popupManager)
        {
            _popupManager = popupManager;
        }

        [Button]
        public void Show()
        {
            _popupManager.Show(_popupType);
        }

        [Button]
        public void Hide()
        {
            _popupManager.Hide();
        }
    }
}
