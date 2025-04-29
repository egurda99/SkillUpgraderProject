using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class PlayerPopupInstallerHelper : MonoBehaviour
    {
        [SerializeField] private PlayerPopupView _playerPopupView;

        public PlayerPopupView PlayerPopupView => _playerPopupView;
    }
}