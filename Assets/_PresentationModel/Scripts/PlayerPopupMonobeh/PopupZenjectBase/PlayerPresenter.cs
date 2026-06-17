using R3;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerLevelPassiveView _playerLevelPassiveView;


        private PlayerLevel _playerLevel;

        private readonly CompositeDisposable
            _disposables = new(); // тк R3 использовалось. а так через обычные ивенты можно было бы


        [Inject]
        public void Construct(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        private void Start()
        {
            _playerLevelPassiveView.UpgradeClicked += OnUpgradeClicked;
            Initialize();
            UpdateView();
        }

        private void Initialize() // подписки
        {
            _playerLevel.CurrentLevelProperty
                .Subscribe(OnLevelUp)
                .AddTo(_disposables);

            _playerLevel.CurrentExperienceProperty
                .Subscribe(OnExperienceChanged)
                .AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _playerLevelPassiveView.UpgradeClicked -= OnUpgradeClicked;
            _disposables.Dispose();
        }

        public void OnUpgradeClicked()
        {
            _playerLevel.LevelUp();
        }


        private void OnExperienceChanged(int obj)
        {
            _playerLevelPassiveView.SetCurrentXp(obj.ToString());
            _playerLevelPassiveView.SetButtonAvailability(_playerLevel.CanLevelUp());
            _playerLevelPassiveView.SetProgressBarStatus(_playerLevel.CanLevelUp());
        }


        private void OnLevelUp(int obj)
        {
            _playerLevelPassiveView.SetNewLevel(obj.ToString());
            UpdateView();
        }

        private void UpdateView()
        {
            _playerLevelPassiveView.SetRequiredXp(_playerLevel.RequiredExperience.ToString());
            _playerLevelPassiveView.SetCurrentXp(_playerLevel.CurrentExperienceProperty.CurrentValue.ToString());
            _playerLevelPassiveView.SetButtonAvailability(_playerLevel.CanLevelUp());
            _playerLevelPassiveView.SetProgressBarStatus(_playerLevel.CanLevelUp());
        }
    }
}
