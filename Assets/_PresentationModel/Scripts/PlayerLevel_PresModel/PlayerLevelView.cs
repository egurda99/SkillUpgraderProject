using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentLevelValueText;
        [SerializeField] private TextMeshProUGUI _currentXp;
        [SerializeField] private TextMeshProUGUI _requiredXp;

        [SerializeField] private LevelButton _levelButton;
        [SerializeField] private LevelProgressBar _levelProgressBar;


        private IPlayerLevelPresentationModel _playerLevelPresenter;


        [Inject]
        public void Construct(PlayerPresentationModel playerPresentationModel)
        {
            _playerLevelPresenter = playerPresentationModel;
        }

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IPlayerLevelPresentationModel playerLevelPresentationModel)
            {
                throw new Exception("Expected playerLevelPresentationModel");
            }

            _playerLevelPresenter = playerLevelPresentationModel;

            UpdateUI();

            _levelButton.SetAvailable(_playerLevelPresenter.CanUpgrade());
            _levelButton.AddListener(OnUpgradeClicked);

            _levelProgressBar.SetStatus(_playerLevelPresenter.CanUpgrade());


            _playerLevelPresenter.OnStateChanged += OnStateChanged;
        }


        public void Hide()
        {
            _playerLevelPresenter.OnStateChanged -= OnStateChanged;
            _levelButton.RemoveListener(OnUpgradeClicked);
        }


        private void OnUpgradeClicked()
        {
            _playerLevelPresenter.OnUpgradeClicked();
        }

        private void OnStateChanged()
        {
            UpdateUI();
            _levelButton.SetAvailable(_playerLevelPresenter.CanUpgrade());
            _levelProgressBar.SetStatus(_playerLevelPresenter.CanUpgrade());
        }


        private void UpdateUI()
        {
            _currentLevelValueText.text = _playerLevelPresenter.GetCurrentLevel();
            _currentXp.text = _playerLevelPresenter.GetCurrentXp();
            _requiredXp.text = _playerLevelPresenter.GetRequiredXp();
        }
    }
}
