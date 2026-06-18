using System;
using Modules.Popups;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsPopupPresenterZenject : PopupPresenter
    {
        [SerializeField] private PlayerStatsPresenterZenject _presenter;
        [SerializeField] private PopupAnimationView _popupAnimationView;

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
