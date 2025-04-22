using R3;
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

        private readonly CompositeDisposable _disposables = new();
        private IPlayerLevelPresentationModel _playerLevelPresenter;


        [Inject]
        public void Construct(PlayerPresentationModel playerPresentationModel)
        {
            _playerLevelPresenter = playerPresentationModel;
        }

        public void Show()
        {
            UpdateUI();

            _levelButton.SetAvailable(_playerLevelPresenter.CanUpgrade());
            _levelButton.AddListener(OnUpgradeClicked);

            _levelProgressBar.SetStatus(_playerLevelPresenter.CanUpgrade());


            _playerLevelPresenter.OnStateChanged
                .Subscribe(_ => OnStateChanged())
                .AddTo(_disposables);
        }

        public void Hide()
        {
            _levelButton.RemoveListener(OnUpgradeClicked);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
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
