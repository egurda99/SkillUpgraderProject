using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerStatsPassiveView _view;

        private PlayerLevel _playerLevel;

        private void Start()
        {
            _playerLevel = ServiceLocator.ServiceLocator.Instance.Get<PlayerLevel>();
        }

        public void Show()
        {
            if (_playerLevel == null)
                _playerLevel = ServiceLocator.ServiceLocator.Instance.Get<PlayerLevel>();

            _view.SetStats($"Level: {_playerLevel.CurrentLevelProperty.CurrentValue}\n" +
                           $"XP: {_playerLevel.CurrentExperienceProperty.CurrentValue} / {_playerLevel.RequiredExperience}");
        }

        public void Hide()
        {
        }
    }
}
