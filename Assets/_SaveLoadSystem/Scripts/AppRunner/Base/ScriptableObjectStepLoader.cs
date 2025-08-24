using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.AppRunner
{
    public sealed class ScriptableObjectStepLoader : LoadingStep
    {
        [SerializeField] private LoadingStepScriptableObject _step;


        public override string Title => _step.Title;

        public override async UniTask Do()
        {
            await _step.Do();
        }
    }
}