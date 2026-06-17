using System;
using UnityEngine;

namespace Modules.Popups
{
    [Serializable]
    public sealed class PopupInfo
    {
        [field: SerializeField]
        public PopupType Type { get; private set; }

        [field: SerializeField]
        public bool Cached { get; private set; }

        [field: SerializeField]
        public PopupPresenter Prefab { get; private set; }
    }
}