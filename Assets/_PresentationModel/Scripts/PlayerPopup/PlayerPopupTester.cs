using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupTester : MonoBehaviour
    {
        private PopupManager _popupManager;

        [Inject]
        public void Construct(PopupManager popupManager)
        {
            _popupManager = popupManager;
        }

        [Button]
        public void Show()
        {
            _popupManager.ShowPopup(PopupName.PLAYER_STATS);
        }

        [Button]
        public void Hide()
        {
            _popupManager.HidePopup(PopupName.PLAYER_STATS);
        }
    }
}
