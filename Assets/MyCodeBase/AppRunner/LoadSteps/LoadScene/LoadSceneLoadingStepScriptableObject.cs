//using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Code.AppRunner
{
    [CreateAssetMenu(fileName = "LoadSceneLoadingStep",
        menuName = "AppRunner/LoadingStepScriptableObject)")]
    public sealed class LoadSceneScriptableObject : LoadingStepScriptableObject
    {
        [SerializeField] private float _waitTime = 0.5f;
        [SerializeField] private string _title = "Loading level";
        [SerializeField] private int _nextSceneBuildIdx;

        public override string Title => _title;

        // public override async UniTask Do()
        // {
        //     await SceneManager.LoadSceneAsync(_nextSceneBuildIdx, LoadSceneMode.Additive);
        //     SceneManager.UnloadSceneAsync(0);
        //     SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_nextSceneBuildIdx));
        //     await UniTask.Delay((int)(_waitTime * 1000));
        // }
    }
}
