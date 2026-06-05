using System;
using TMPro;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelPassiveView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentLevelValueText;
        [SerializeField] private TextMeshProUGUI _currentXp;
        [SerializeField] private TextMeshProUGUI _requiredXp;

        [SerializeField] private LevelButtonTest _levelButton;
        [SerializeField] private LevelProgressBar _levelProgressBar;

        public event Action UpgradeClicked;

        private void Start()
        {
            _levelButton.AddListener(OnUpgradeClicked);
        }

        private void OnDestroy()
        {
            _levelButton.RemoveListener(OnUpgradeClicked);
        }

        private void OnUpgradeClicked()
        {
            UpgradeClicked?.Invoke();
        }

        public void SetButtonAvailability(bool isAvailable)
        {
            _levelButton.SetAvailable(isAvailable);
        }

        public void SetProgressBarStatus(bool isAvailable)
        {
            _levelProgressBar.SetStatus(isAvailable);
        }


        public void SetCurrentXp(string currentXp)
        {
            _currentXp.text = currentXp;
        }

        public void SetRequiredXp(string requiredXp)
        {
            _requiredXp.text = requiredXp;
        }

        public void SetNewLevel(string newLevel)
        {
            _currentLevelValueText.text = newLevel;
        }
    }
}
