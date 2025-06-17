using System;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [CreateAssetMenu(
        fileName = "UpgradeCatalog",
        menuName = "SO/" + "New UpgradeCatalog"
    )]
    public sealed class UpgradeCatalog : ScriptableObject
    {
        [SerializeField] private UpgradeConfig[] _configs;

        public UpgradeConfig[] GetAllUpgrades()
        {
            return _configs;
        }

        public UpgradeConfig FindUpgrade(string id)
        {
            var length = _configs.Length;
            for (var i = 0; i < length; i++)
            {
                var config = _configs[i];
                if (config.Id == id)
                {
                    return config;
                }
            }

            throw new Exception($"Config with {id} is not found!");
        }
    }
}
