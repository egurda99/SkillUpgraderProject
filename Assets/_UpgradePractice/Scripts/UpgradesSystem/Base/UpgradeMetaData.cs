using System;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [Serializable]
    public sealed class UpgradeMetaData : IUpgradeMetadata
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _title;
        [SerializeField] private string _description;


        public Sprite Icon => _sprite;
        public string Title => _title;
        public string Decription => _description;
    }
}
