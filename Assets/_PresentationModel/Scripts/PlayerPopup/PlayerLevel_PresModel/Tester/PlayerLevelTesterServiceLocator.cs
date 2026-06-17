using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelTesterServiceLocator : MonoBehaviour
    {
        private PlayerLevel _playerLevel;

        private void Start()
        {
            _playerLevel = ServiceLocator.ServiceLocator.Instance.Get<PlayerLevel>();
        }

        [Button]
        public void AddExperience(int range)
        {
            _playerLevel.AddExperience(range);
        }

        [Button]
        public void LevelUp()
        {
            _playerLevel.LevelUp();
        }
    }
}