using R3;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelPresenterNoZenject : MonoBehaviour
    {
        [SerializeField] private PlayerLevelPassiveView _view;

        private PlayerLevel _playerLevel;
        private CompositeDisposable _disposables;

        private void Start()
        {
            _playerLevel = ServiceLocator.ServiceLocator.Instance.Get<PlayerLevel>();
        }

        public void Show()
        {
            if (_playerLevel == null)
            {
                _playerLevel = ServiceLocator.ServiceLocator.Instance.Get<PlayerLevel>();
            }

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
