using System;
using UnityEngine;
using UnityEngine.Events;

namespace MyCodeBase
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnPopupShow;

        [SerializeField] private UnityEvent OnPopupHide;

        public event Action<Popup> OnPopupCloseRequested;

        public event Action OnPopupHided;

        public void Show()
        {
            OnShow();
            OnPopupShow?.Invoke();
        }

        public void Hide()
        {
            OnHide();
            OnPopupHide?.Invoke();
            OnPopupHided?.Invoke();
        }

        public void HideRequested()
        {
            OnPopupCloseRequested?.Invoke(this);
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}
