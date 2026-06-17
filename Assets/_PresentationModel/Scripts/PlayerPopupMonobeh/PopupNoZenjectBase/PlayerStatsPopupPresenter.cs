using System;
using Modules.Popups;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsPopupPresenter : PopupPresenter
    {
        [SerializeField] private PlayerStatsPassiveView _passiveView;
        [SerializeField] private PopupView _popupView; // опционально: назначить для анимаций

        private PlayerLevel _playerLevel;

        private void Awake()
        {
            _playerLevel = ServiceLocator.ServiceLocator.Instance.Get<PlayerLevel>();
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
