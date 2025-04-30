using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupTester : MonoBehaviour
    {
        [SerializeField] private UserInfoTester _userInfoTester;
        [SerializeField] private PlayerLevelTester _playerLevelTester;
        [SerializeField] private StatsTester _statsTester;

        private PopupManager _popupManager;

        [Inject]
        public void Construct(PopupManager popupManager)
        {
            _popupManager = popupManager;
        }

        [Button]
        public void ShowPopup()
        {
            _popupManager.ShowPopup(PopupName.PLAYER_STATS);
        }

        [Button]
        public void HidePopup()
        {
            _popupManager.HidePopup(PopupName.PLAYER_STATS);
        }

        [Button]
        public void ChangeName(string name)
        {
            _userInfoTester.ChangeName(name);
        }

        [Button]
        public void ChangeDescription(string description)
        {
            _userInfoTester.ChangeDescription(description);
        }

        [Button]
        public void ChangeIcon(Sprite icon)
        {
            _userInfoTester.ChangeIcon(icon);
        }

        [Button]
        public void AddExperience(int range)
        {
            _playerLevelTester.AddExperience(range);
        }

        [Button]
        public void LevelUp()
        {
            _playerLevelTester.LevelUp();
        }

        [Button]
        public void LoadDefaultStats()
        {
            _statsTester.LoadDefaultStats();
        }

        [Button]
        public void RemoveDefaultStats()
        {
            _statsTester.RemoveDefaultStats();
        }


        [Button]
        public void AddStat(string name, int value)
        {
            _statsTester.AddStat(name, value);
        }

        [Button]
        public void RemoveStat(string statName)
        {
            _statsTester.RemoveStat(statName);
        }
    }
}
