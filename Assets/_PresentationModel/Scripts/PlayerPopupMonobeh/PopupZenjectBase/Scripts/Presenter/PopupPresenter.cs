using System;
using UnityEngine;

namespace Modules.Popups
{
    public abstract class PopupPresenter : MonoBehaviour
    {
        public abstract void Show(IPopupArgs args);

        public abstract void Hide();

        public virtual void Hide(Action onComplete)
        {
            Hide();
            onComplete?.Invoke();
        }
    }
}
