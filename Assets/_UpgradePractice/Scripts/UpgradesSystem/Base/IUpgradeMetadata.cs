using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public interface IUpgradeMetadata
    {
        public Sprite Icon { get; }

        public string Title { get; }

        public string Decription { get; }
    }
}
