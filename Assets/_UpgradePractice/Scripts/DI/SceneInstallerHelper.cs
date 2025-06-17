using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class SceneInstallerHelper : MonoBehaviour
    {
        [SerializeField] private UpgradeCatalog _upgradeCatalog;

        public UpgradeCatalog UpgradeCatalog => _upgradeCatalog;
    }
}