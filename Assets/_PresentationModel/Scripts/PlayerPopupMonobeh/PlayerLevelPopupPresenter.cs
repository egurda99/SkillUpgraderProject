using R3;
using UnityEngine;
using Modules.PopupsStandalone;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelPopupPresenter : PopupPresenter
    {
        [SerializeField] private PlayerLevelPassiveView _view;

        private PlayerLevel _playerLevel;
        private CompositeDisposable _disposables;

        private void Awake()
        {
            _playerLevel = ServiceLocator.ServiceLocator.Instance.Get<PlayerLevel>();
        }

        public override void Show(IPopupArgs args)
        {
            _view.gameObject.SetActive(true);
            _view.UpgradeClicked += OnUpgradeClicked;

            _disposables = new CompositeDisposable();
            _playerLevel.CurrentLevelProperty.Subscribe(_ => RefreshView()).AddTo(_disposables);
            _playerLevel.CurrentExperienceProperty.Subscribe(_ => RefreshView()).AddTo(_disposables);

            RefreshView();
        }

        public override void Hide()
        {
            _view.UpgradeClicked -= OnUpgradeClicked;
            _disposables?.Dispose();
            _view.gameObject.SetActive(false);
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
