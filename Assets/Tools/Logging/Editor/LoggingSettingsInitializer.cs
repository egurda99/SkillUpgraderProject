using Tools.Logging;
using UnityEditor;
using UnityEngine;

namespace Logging.Editor
{
    public static class LoggingSettingsInitializer
    {
        [InitializeOnLoadMethod]
        private static void CreateSettingsIfNecessary()
        {
            if (AssetDatabase.AssetPathExists(LoggingPaths.SettingsAssetPath))
            {
                return;
            }

            var settingsAsset = CreateSettingsAsset();
            LogApiGenerator.Generate(settingsAsset, LoggingPaths.GeneratedLogPath);
        }

        internal static void RegenerateLogFile()
        {
            if (!AssetDatabase.AssetPathExists(LoggingPaths.SettingsAssetPath))
            {
                CreateSettingsIfNecessary();
                return;
            }

            var settings = AssetDatabase.LoadAssetAtPath<LoggingSettings>(LoggingPaths.SettingsAssetPath);
            if (settings != null)
            {
                LogApiGenerator.Generate(settings, LoggingPaths.GeneratedLogPath);
            }
        }

        private static LoggingSettings CreateSettingsAsset()
        {
            var settingsAsset = ScriptableObject.CreateInstance<LoggingSettings>();

            if (!AssetDatabase.IsValidFolder(LoggingPaths.ResourcesFolderPath))
            {
                AssetDatabase.CreateFolder(LoggingPaths.MainFolder, "Resources");
            }

            AssetDatabase.CreateAsset(settingsAsset, LoggingPaths.SettingsAssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return settingsAsset;
        }
    }
}
