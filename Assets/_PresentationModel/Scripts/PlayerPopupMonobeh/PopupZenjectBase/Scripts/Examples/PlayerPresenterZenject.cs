using R3;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPresenterZenject : MonoBehaviour
    {
        [SerializeField] private PlayerLevelPassiveView _view;

        private PlayerLevel _playerLevel;
        private CompositeDisposable _disposables;

        [Inject]
        public void Construct(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        public void Show()
        {
            _view.UpgradeClicked += OnUpgradeClicked;

            _disposables = new CompositeDisposable();
            _playerLevel.CurrentLevelProperty.Subscribe(_ => RefreshView()).AddTo(_disposables);
            _playerLevel.CurrentExperienceProperty.Subscribe(_ => RefreshView()).AddTo(_disposables);
            RefreshView();
        }

        public void Hide()
        {
            _view.UpgradeClicked -= OnUpgradeClicked;
            _disposables?.Dispose();
        }

        private void OnUpgradeClicked()
        {
            _playerLevel.LevelUp();
        }

        private void RefreshView()
        {
            _view.SetNewLevel(_playerLevel.CurrentLevelProperty.CurrentValue.ToString());
            _view.SetCurrentXp(_playerLevel.CurrentExperienceProperty.CurrentValue.ToString());
            _view.SetRequiredXp(_playerLevel.RequiredExperience.ToString());
            _view.SetButtonAvailability(_playerLevel.CanLevelUp());
            _view.SetProgressBarStatus(_playerLevel.CanLevelUp());
        }
    }
}
