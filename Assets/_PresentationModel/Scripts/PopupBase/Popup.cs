using System;
using UnityEngine;
using UnityEngine.Events;

namespace Lessons.Architecture.PM
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnPopupShow;
        [SerializeField] private UnityEvent OnPopupHide;

        public event Action<Popup> OnPopupCloseRequested;

        public IPopupViewModel ViewModel => _viewModel;

        private IPopupViewModel _viewModel;

        public void Show(IPopupViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.Show();
            OnShow();
            OnPopupShow?.Invoke();
        }

        public void Hide()
        {
            _viewModel?.Hide();
            OnHide();
            OnPopupHide?.Invoke();
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

        protected virtual void OnDestroy()
        {
            _viewModel?.Dispose();
            _viewModel = null;
        }
    }
}
