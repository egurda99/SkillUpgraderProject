using System;
using Modules.Popups;
using R3;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelPopupPresenterZenject : PopupPresenter
    {
        [SerializeField] private PlayerLevelPassiveView _view;
        [SerializeField] private PopupView _popupView;

        private PlayerLevel _playerLevel;
        private CompositeDisposable _disposables;

        [Inject]
        public void Construct(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        public override void Show(IPopupArgs args)
        {
            _view.UpgradeClicked += OnUpgradeClicked;

            _disposables = new CompositeDisposable();
            _playerLevel.CurrentLevelProperty.Subscribe(_ => RefreshView()).AddTo(_disposables);
            _playerLevel.CurrentExperienceProperty.Subscribe(_ => RefreshView()).AddTo(_disposables);
            RefreshView();

            if (_popupView != null)
                _popupView.AnimateShow();
            else
                _view.gameObject.SetActive(true);
        }

        public override void Hide()
        {
            Cleanup();
            if (_popupView != null)
                _popupView.Hide();
            else
                _view.gameObject.SetActive(false);
        }

        public override void Hide(Action onComplete)
        {
            Cleanup();
            if (_popupView != null)
                _popupView.AnimateHide(() => onComplete?.Invoke());
            else
            {
                _view.gameObject.SetActive(false);
                onComplete?.Invoke();
            }
        }

        private void Cleanup()
        {
            _view.UpgradeClicked -= OnUpgradeClicked;
            _disposables?.Dispose();
        }

        private void OnUpgradeClicked() => _playerLevel.LevelUp();

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
