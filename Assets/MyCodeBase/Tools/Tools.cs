using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace MyCodeBase
{
    public static class Tools
    {
        private const string FileName = "gamestate.sav";

        [MenuItem("Tools/Open GameState File")]
        public static void OpenGameStateFile()
        {
            var filePath = Path.Combine(Application.persistentDataPath, FileName);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"File not found at path: {filePath}");
                return;
            }

            OpenFileInExplorer(filePath);
        }

        private static void OpenFileInExplorer(string filePath)
        {
#if UNITY_EDITOR_WIN
            filePath = filePath.Replace('/', '\\'); // Windows формат
            var argument = $"/select,\"{filePath}\"";
            Process.Start("explorer.exe", argument);
#elif UNITY_EDITOR_OSX
    Process.Start("open", $"-R \"{filePath}\"");
#else
    UnityEngine.Debug.LogWarning("Opening files is not supported on this platform.");
#endif
        }

        [MenuItem("Tools/Clear PlayerPrefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            UnityEngine.Debug.Log("PlayerPrefs cleared.");
        }
    }
}
