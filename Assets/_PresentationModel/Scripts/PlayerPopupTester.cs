using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupTester : MonoBehaviour
    {
        [SerializeField] private PlayerPopup _playerPopup;

        [Button]
        public void Show()
        {
            _playerPopup.gameObject.SetActive(true);
            _playerPopup.Show();
        }

        [Button]
        public void Hide()
        {
            _playerPopup.gameObject.SetActive(false);
            _playerPopup.Hide();
        }
    }
}
