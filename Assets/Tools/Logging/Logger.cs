using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Tools.Logging
{
    public sealed class Logger
    {
        public const string DEFINE_SYMBOL = "LOGS_ENABLED";

        private readonly string _tag;
        private bool _isInfoEnabled = true;
        private bool _isWarningEnabled = true;
        private Color? _color;

        public static event Action<string> OnError;

        public Logger(string tag)
        {
            _tag = tag;
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional(DEFINE_SYMBOL)]
        public void Info(object msg)
        {
            if (_isInfoEnabled)
            {
                Debug.Log(FormatMessage(msg));
            }
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional(DEFINE_SYMBOL)]
        public void Warning(object msg)
        {
            if (_isWarningEnabled)
            {
                Debug.LogWarning(FormatMessage(msg));
            }
        }

        public void Error(object msg)
        {
            var message = FormatMessage(msg);
            Debug.LogError(message);
            OnError?.Invoke(message);
        }

        public void ApplySettings(LoggingSettings settings)
        {
            var definition = settings.GetChannelDefinition(_tag);

            if (definition == null)
            {
                Error($"[{_tag}] No channel definition found for {_tag}. Logs enabled");
                return;
            }

            _isInfoEnabled = !definition.MuteInfo;
            _isWarningEnabled = !definition.MuteWarning;
            _color = definition.Color;
        }

        private string FormatMessage(object msg)
        {
            var text = $"[{_tag}] {msg}";

            if (!_color.HasValue)
            {
                return text;
            }

            var hexColor = ColorUtility.ToHtmlStringRGBA(_color.Value);
            return $"<color=#{hexColor}>{text}</color>";
        }
    }
}
