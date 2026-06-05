using System;
using UnityEngine;

namespace Modules.PopupsStandalone
{
    [CreateAssetMenu(
        fileName = "PopupCatalog",
        menuName = "Modules/PopupsStandalone/New PopupCatalog"
    )]
    public sealed class PopupCatalog : ScriptableObject
    {
        [SerializeField]
        private PopupInfo[] _presenters;

        public PopupInfo GetPopupInfo<T>() where T : PopupPresenter
        {
            foreach (PopupInfo info in _presenters)
                if (info.Prefab is T)
                    return info;

            throw new Exception($"Info for popup of type {typeof(T)} not found in {name}");
        }
    }
}
