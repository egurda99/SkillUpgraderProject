using System;
using Modules.Popups;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsPopupPresenter : PopupPresenter
    {
        [SerializeField] private PlayerStatsPresenter _presenter;
        [SerializeField] private PopupAnimationView _popupAnimationView; // опционально: назначить для анимаций

        public override void Show(IPopupArgs args)
        {
            _presenter.Show();

            if (_popupAnimationView != null)
                _popupAnimationView.AnimateShow();
        }

        public override void Hide()
        {
            _presenter.Hide();
            _popupAnimationView?.Hide();
        }

        public override void Hide(Action onComplete)
        {
            _presenter.Hide();

            if (_popupAnimationView != null)
                _popupAnimationView.AnimateHide(() => onComplete?.Invoke());
            else
                onComplete?.Invoke();
        }
    }
}
