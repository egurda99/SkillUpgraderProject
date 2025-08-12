using UnityEditor;

namespace AssetManager.Examples
{
    public sealed class ApplicationExiter
    {
        public void ExitApp()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            //Application.Quit(0);
#endif
        }
    }
}
