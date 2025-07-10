using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.AppRunner
{
    public sealed class LoadingUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private TMP_Text _progressTitle;

        public void Setup(int totalSteps)
        {
            _slider.maxValue = totalSteps;
            _slider.value = 0;
            _progressText.text = "0%";
            _progressTitle.text = "";
        }

        public void UpdateProgress(int currentStep, int totalSteps, string title)
        {
            var progress = (float)currentStep / totalSteps;
            _slider.value = currentStep;
            _progressText.text = $"{progress * 100:0.00}%";
            _progressTitle.text = title;
        }
    }
}