using Tools.Logging;
using UnityEditor;
using UnityEngine;

namespace Logging.Editor
{
    public static class LoggingEditorMenu
    {
        [MenuItem("Tools/Logging/Regenerate Log File")]
        private static void RegenerateLogFile()
        {
            LoggingSettingsInitializer.RegenerateLogFile();
        }

        [MenuItem("Tools/Logging/Settings")]
        private static void OpenSettings()
        {
            var asset = AssetDatabase.LoadAssetAtPath<LoggingSettings>(LoggingPaths.SettingsAssetPath);

            if (asset == null)
            {
                Debug.LogWarning($"Asset not found: {LoggingPaths.SettingsAssetPath}");
                return;
            }

            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
        }
    }
}
