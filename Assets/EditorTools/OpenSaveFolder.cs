using UnityEditor;

public static class OpenSaveFolder
{
    [MenuItem("Tools/Open Save Folder")]
    private static void OpenFolderMenuItem()
    {
        SaveFileHelper.OpenSaveFolder();
    }
}