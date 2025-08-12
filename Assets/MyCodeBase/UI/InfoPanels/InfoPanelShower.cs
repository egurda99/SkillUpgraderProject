using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyCodeBase.UI
{
    [Serializable]
    public class InfoPanelShower
    {
        [SerializeField] private InfoPanelView _viewPrefab;

        protected InfoPanelView view;

        public void Show(Transform parent)
        {
            view = Object.Instantiate(_viewPrefab, parent);
            OnShow();
        }

        public void Hide()
        {
            if (view != null)
            {
                OnHide();
                Object.Destroy(view.gameObject);
            }
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}
