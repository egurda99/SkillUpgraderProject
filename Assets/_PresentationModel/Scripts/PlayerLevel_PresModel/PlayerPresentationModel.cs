using System;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPresentationModel : IPlayerLevelPresentationModel, IInitializable, IDisposable
    {
        public event Action OnStateChanged;

        private readonly PlayerLevel _playerLevel;

        public PlayerPresentationModel(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        public void Initialize()
        {
            _playerLevel.OnLevelUp += OnLevelUp;
            _playerLevel.OnExperienceChanged += OnExperienceChanged;
        }

        public void Dispose()
        {
            _playerLevel.OnLevelUp -= OnLevelUp;
            _playerLevel.OnExperienceChanged -= OnExperienceChanged;
        }


        public string GetCurrentLevel()
        {
            return _playerLevel.CurrentLevel.ToString();
        }

        public string GetCurrentXp()
        {
            return _playerLevel.CurrentExperience.ToString();
        }

        public string GetRequiredXp()
        {
            return _playerLevel.RequiredExperience.ToString();
        }

        public bool CanUpgrade()
        {
            return _playerLevel.CanLevelUp();
        }

        public void OnUpgradeClicked()
        {
            _playerLevel.LevelUp();
        }


        private void OnExperienceChanged(int obj)
        {
            OnStateChanged?.Invoke();
        }


        private void OnLevelUp()
        {
            OnStateChanged?.Invoke();
        }
    }
}
