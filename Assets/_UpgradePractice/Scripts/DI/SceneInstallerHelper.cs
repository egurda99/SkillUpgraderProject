using System.Collections.Generic;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class SceneInstallerHelper : MonoBehaviour
    {
        [SerializeField] private UpgradeCatalog _upgradeCatalog;
        [SerializeField] private List<ResourceExchangeRate> _resourceExchangeRates;

        public UpgradeCatalog UpgradeCatalog => _upgradeCatalog;

        public List<ResourceExchangeRate> ResourceExchangeRates => _resourceExchangeRates;
    }
}
