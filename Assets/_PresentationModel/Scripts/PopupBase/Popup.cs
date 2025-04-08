using UnityEngine;
using UnityEngine.Events;

namespace Lessons.Architecture.PM
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnPopupShow;

        [SerializeField] private UnityEvent OnPopupHide;

        public void Show()
        {
            OnShow();
            OnPopupShow?.Invoke();
        }

        public void Hide()
        {
            OnHide();
            OnPopupHide?.Invoke();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}