using System.Collections.Generic;
using UnityEngine;

namespace Tools.Logging
{
    public class LoggingSettings : ScriptableObject
    {
        public const string FileName = "LoggingSettings";

        [SerializeField] private LogChannelDefinition[] _channels = { };

        private readonly Dictionary<string, LogChannelDefinition> _channelsMap = new();
        private bool _isInitialized;

        public IReadOnlyList<LogChannelDefinition> Channels => _channels;

        public LogChannelDefinition GetChannelDefinition(string tag)
        {
            EnsureInitialization();

            return _channelsMap.GetValueOrDefault(tag);
        }

        private void EnsureInitialization()
        {
            if (_isInitialized) return;

            _channelsMap.Clear();

            foreach (var channel in _channels)
            {
                _channelsMap[channel.Tag] = channel;
            }
        }
    }
}
