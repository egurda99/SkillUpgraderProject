using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class UpgradesDebugger : MonoBehaviour
    {
        private UpgradesManager _upgradeManager;

        [Inject]
        public void Construct(UpgradesManager upgradesManager)
        {
            _upgradeManager = upgradesManager;
        }

        [Button]
        public bool CanLevelUp(string id)
        {
            return _upgradeManager.CanLevelUp(id);
        }

        [Button]
        public void LevelUp(string id)
        {
            _upgradeManager.LevelUp(id);
        }
    }
}
