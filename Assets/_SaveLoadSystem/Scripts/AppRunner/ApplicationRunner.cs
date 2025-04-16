using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.AppRunner
{
    public sealed class ApplicationRunner : MonoBehaviour
    {
        [SerializeField] private List<StepApplicationRunner> _stepApplicationRunner;
        [SerializeField] public int _nextSceneBuildIdx = 1;
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private TMP_Text _progressTitle;
        
        private void Start()
        {
            StartAllStep().Forget();
        }

        private async UniTaskVoid StartAllStep()
        {
            int countStep = _stepApplicationRunner.Count;
            _slider.maxValue = countStep;
            for (var index = 0; index < countStep; index++)
            {
                StepApplicationRunner step = _stepApplicationRunner[index];
                _progressTitle.text = step.Title;
                float progress = index + 1;
                _slider.value = progress;
                _progressText.text = $"{progress / countStep * 100:0.00}%";
                await step.WaitOnCompleted();
            }
            RunScene().Forget();
        }

        private async UniTaskVoid RunScene()
        {
            await SceneManager.LoadSceneAsync(_nextSceneBuildIdx, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(0);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_nextSceneBuildIdx)); 
        }
    }
}
