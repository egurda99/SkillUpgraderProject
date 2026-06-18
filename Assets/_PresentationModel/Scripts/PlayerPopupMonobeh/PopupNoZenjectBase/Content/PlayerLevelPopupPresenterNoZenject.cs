using System;
using Modules.Popups;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelPopupPresenterNoZenject : PopupPresenter
    {
        [SerializeField] private PlayerLevelPresenterNoZenject _presenterNoZenject;
        [SerializeField] private PopupAnimationView _popupAnimationView; // опционально: назначить для анимаций

        public override void Show(IPopupArgs args)
        {
            _presenterNoZenject.Show();

            if (_popupAnimationView != null)
                _popupAnimationView.AnimateShow();
        }

        public override void Hide()
        {
            _presenterNoZenject.Hide();
            _popupAnimationView?.Hide();
        }

        public override void Hide(Action onComplete)
        {
            _presenterNoZenject.Hide();

            if (_popupAnimationView != null)
                _popupAnimationView.AnimateHide(() => onComplete?.Invoke());
            else
                onComplete?.Invoke();
        }
    }
}
