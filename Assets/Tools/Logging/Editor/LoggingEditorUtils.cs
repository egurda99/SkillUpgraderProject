using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Compilation;
using UnityEngine;
using Logger = Tools.Logging.Logger;

namespace Logging.Editor
{
    internal static class LoggingEditorUtils
    {
        private static readonly IEnumerable<NamedBuildTarget> AllTargets = new[]
        {
            NamedBuildTarget.Standalone,
            NamedBuildTarget.Android,
            NamedBuildTarget.iOS,
            NamedBuildTarget.WebGL,
            NamedBuildTarget.NintendoSwitch,
            NamedBuildTarget.PS4,
            NamedBuildTarget.PS5,
            NamedBuildTarget.Server,
            NamedBuildTarget.XboxOne,
            NamedBuildTarget.EmbeddedLinux
        };

        internal static void EnableBuildLogs()
        {
            CompilationPipeline.compilationFinished += OnCompilationFinishedAfterEnabling;

            foreach (var target in AllTargets)
            {
                AddDefineSymbol(target, Logger.DEFINE_SYMBOL);
            }
        }

        internal static void DisableBuildLogs()
        {
            CompilationPipeline.compilationFinished += OnCompilationFinishedAfterDisabling;

            foreach (var target in AllTargets)
            {
                RemoveDefineSymbol(target, Logger.DEFINE_SYMBOL);
            }
        }

        internal static bool IsReleaseBuildLogEnabled()
        {
            foreach (var target in AllTargets)
            {
                var defines = PlayerSettings.GetScriptingDefineSymbols(target);
                if (!defines.Contains(Logger.DEFINE_SYMBOL))
                {
                    return false;
                }
            }

            return true;
        }

        private static void AddDefineSymbol(NamedBuildTarget target, string definition)
        {
            var defines = PlayerSettings.GetScriptingDefineSymbols(target);

            if (defines.Contains(definition)) return;

            if (!string.IsNullOrEmpty(defines))
                defines += ";";

            defines += definition;

            PlayerSettings.SetScriptingDefineSymbols(target, defines);
        }

        private static void RemoveDefineSymbol(NamedBuildTarget target, string definition)
        {
            var defines = PlayerSettings
                .GetScriptingDefineSymbols(target)
                .Split(';')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (defines.Remove(definition))
            {
                PlayerSettings.SetScriptingDefineSymbols(target, string.Join(";", defines));
            }
        }

        private static void OnCompilationFinishedAfterDisabling(object o)
        {
            CompilationPipeline.compilationFinished -= OnCompilationFinishedAfterDisabling;
            Debug.Log("Build Logging disabled");
        }

        private static void OnCompilationFinishedAfterEnabling(object o)
        {
            CompilationPipeline.compilationFinished -= OnCompilationFinishedAfterEnabling;
            Debug.Log("Build Logging Enabled");
        }
    }
}
