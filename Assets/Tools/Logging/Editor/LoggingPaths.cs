using Tools.Logging;

namespace Logging.Editor
{
    internal static class LoggingPaths
    {
        public const string MainFolder = "Assets/Tools/Logging";
        public static readonly string ResourcesFolderPath = $"{MainFolder}/Resources";
        public static readonly string GeneratedLogPath = $"{MainFolder}/Log.Generated.cs";
        public static readonly string SettingsAssetPath = $"{ResourcesFolderPath}/{LoggingSettings.FileName}.asset";
    }
}
