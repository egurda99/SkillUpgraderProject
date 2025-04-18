using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class SaveFileHelper
{
    public static void OpenSaveFolder()
    {
        var path = Application.persistentDataPath;

#if UNITY_EDITOR
        EditorUtility.RevealInFinder(path);
#elif UNITY_STANDALONE_WIN
        Process.Start("explorer.exe", path.Replace("/", "\\"));
#elif UNITY_STANDALONE_OSX
        Process.Start("open", path);
#else
        Debug.LogWarning("OpenSaveFolder not supported on this platform.");
#endif

        Debug.Log($" Opened save folder: {path}");
    }
}
