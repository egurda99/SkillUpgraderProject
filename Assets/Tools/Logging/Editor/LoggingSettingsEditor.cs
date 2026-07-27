using Tools.Logging;
using UnityEditor;
using UnityEngine;

namespace Logging.Editor
{
    [CustomEditor(typeof(LoggingSettings))]
    public sealed class LoggingSettingsEditor : UnityEditor.Editor
    {
        private bool _enableReleaseBuildLogsCached;

        private void OnEnable()
        {
            _enableReleaseBuildLogsCached = LoggingEditorUtils.IsReleaseBuildLogEnabled();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            _enableReleaseBuildLogsCached =
                EditorGUILayout.Toggle("Enable Release Build Logs", _enableReleaseBuildLogsCached);

            if (EditorGUI.EndChangeCheck())
            {
                if (_enableReleaseBuildLogsCached)
                {
                    LoggingEditorUtils.EnableBuildLogs();
                }
                else
                {
                    LoggingEditorUtils.DisableBuildLogs();
                }
            }

            EditorGUILayout.Space();

            if (serializedObject.hasModifiedProperties)
            {
                EditorGUILayout.HelpBox("Apply inspector changes before generating Log API.", MessageType.Info);
            }

            using (new EditorGUI.DisabledScope(serializedObject.hasModifiedProperties))
            {
                if (GUILayout.Button("Regenerate Log API"))
                {
                    LogApiGenerator.Generate((LoggingSettings)target, LoggingPaths.GeneratedLogPath);
                }
            }
        }
    }
}
