using UnityEngine;

namespace Modules.Popups
{
    public abstract class PopupPresenter : MonoBehaviour
    {
        protected internal PopupManager Manager { get; set; }

        public abstract void Show(IPopupArgs args);

        public abstract void Hide();
    }
}