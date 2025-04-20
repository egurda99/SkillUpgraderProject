using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.AppRunner
{
    public sealed class ApplicationRunner : MonoBehaviour
    {
        [SerializeField] private List<LoadingStep> _stepApplicationRunner;
        [SerializeField] private LoadingUI _loadingUI;


        private void Start()
        {
            StartAllStep().Forget();
        }

        private async UniTaskVoid StartAllStep()
        {
            var countStep = _stepApplicationRunner.Count;
            _loadingUI.Setup(countStep);
            for (var index = 0; index < countStep; index++)
            {
                var loadingStep = _stepApplicationRunner[index];
                _loadingUI.UpdateProgress(index + 1, countStep, loadingStep.Title);
                await loadingStep.Do();
            }
        }
    }
}
