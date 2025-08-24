using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.AppRunner
{
    public sealed class LoadingSceneStep : LoadingStep
    {
        [SerializeField] private LoadSceneScriptableObject _loadSceneScriptableObject;
        public override string Title => _loadSceneScriptableObject.Title;

        public override async UniTask Do()
        {
            await _loadSceneScriptableObject.Do();
        }
    }
}
