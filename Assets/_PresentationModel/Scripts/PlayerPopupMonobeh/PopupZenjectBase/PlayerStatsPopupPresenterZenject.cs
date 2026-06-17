using System;
using Modules.Popups;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsPopupPresenterZenject : PopupPresenter
    {
        [SerializeField] private PlayerStatsPassiveView _passiveView;
        [SerializeField] private PopupView _popupView;

        private PlayerLevel _playerLevel;

        [Inject]
        public void Construct(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        public override void Show(IPopupArgs args)
        {
            _passiveView.SetStats($"Level: {_playerLevel.CurrentLevelProperty.CurrentValue}\n" +
                                  $"XP: {_playerLevel.CurrentExperienceProperty.CurrentValue} / {_playerLevel.RequiredExperience}");

            if (_popupView != null)
                _popupView.AnimateShow();
            else
                _passiveView.gameObject.SetActive(true);
        }

        public override void Hide()
        {
            if (_popupView != null)
                _popupView.Hide();
            else
                _passiveView.gameObject.SetActive(false);
        }

        public override void Hide(Action onComplete)
        {
            if (_popupView != null)
                _popupView.AnimateHide(() => onComplete?.Invoke());
            else
            {
                _passiveView.gameObject.SetActive(false);
                onComplete?.Invoke();
            }
        }
    }
}
