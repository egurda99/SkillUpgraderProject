using Modules.Popups;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.PopupsStandalone
{
    public sealed class PopupTesterNoZenject : MonoBehaviour
    {
        [SerializeField] private PopupManagerNoZenject _popupManager;
        [SerializeField] private PopupType _popupType;

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
