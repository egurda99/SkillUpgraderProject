using System;
using R3;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPresentationModel : IPlayerLevelPresentationModel, IInitializable, IDisposable
    {
        private readonly PlayerLevel _playerLevel;

        private readonly CompositeDisposable _disposables = new();
        private readonly Subject<Unit> _onStateChanged = new();

        public PlayerPresentationModel(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        public void Initialize()
        {
            _playerLevel.CurrentLevelProperty
                .Subscribe(OnLevelUp)
                .AddTo(_disposables);

            _playerLevel.CurrentExperienceProperty
                .Subscribe(OnExperienceChanged)
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }


        Observable<Unit> IPlayerLevelPresentationModel.OnStateChanged => _onStateChanged;

        public string GetCurrentLevel()
        {
            return _playerLevel.CurrentLevelProperty.CurrentValue.ToString();
        }

        public string GetCurrentXp()
        {
            return _playerLevel.CurrentExperienceProperty.CurrentValue.ToString();
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
            _onStateChanged.OnNext(Unit.Default);
        }


        private void OnLevelUp(int obj)
        {
            _onStateChanged.OnNext(Unit.Default);
        }
    }
}
