using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupInstallerHelper : MonoBehaviour
    {
        [SerializeField] private PlayerLevelView _playerLevelView;

        public PlayerLevelView PlayerLevelView => _playerLevelView;
    }
}