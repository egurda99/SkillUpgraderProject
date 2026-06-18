using System;
using Modules.Popups;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelPopupPresenterZenject : PopupPresenter
    {
        [SerializeField] private PlayerPresenterZenject _presenterZenject;
        [SerializeField] private PopupAnimationView _popupAnimationView;

        public override void Show(IPopupArgs args)
        {
            _presenterZenject.Show();

            if (_popupAnimationView != null)
                _popupAnimationView.AnimateShow();
        }

        public override void Hide()
        {
            _presenterZenject.Hide();
            _popupAnimationView?.Hide();
        }

        public override void Hide(Action onComplete)
        {
            _presenterZenject.Hide();

            if (_popupAnimationView != null)
                _popupAnimationView.AnimateHide(() => onComplete?.Invoke());
            else
                onComplete?.Invoke();
        }
    }
}
