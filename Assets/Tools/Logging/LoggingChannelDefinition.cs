using System;
using UnityEngine;

namespace Tools.Logging
{
    [Serializable]
    public sealed class LogChannelDefinition
    {
        [SerializeField] private string _propertyName;
        [SerializeField] private string _tag;
        [SerializeField] private bool _muteInfo = true;
        [SerializeField] private bool _muteWarning = true;
        [SerializeField] private Color _color = Color.grey;

        public string PropertyName => _propertyName;
        public string Tag => _tag;
        public bool MuteInfo => _muteInfo;
        public bool MuteWarning => _muteWarning;
        public Color Color => _color;
    }
}
