using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelTester : MonoBehaviour
    {
        private PlayerLevel _playerLevel;

        [Inject]
        public void Construct(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
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