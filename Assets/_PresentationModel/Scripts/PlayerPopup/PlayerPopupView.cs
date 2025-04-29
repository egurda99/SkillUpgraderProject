using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupView : MonoBehaviour
    {
        [SerializeField] private PlayerLevelView _playerLevelView;
        [SerializeField] private UserInfoView _userInfoView;

        public UserInfoView UserInfoView => _userInfoView;

        private StatListView _statListView;
        private PlayerPopupViewModel _viewModel;
        private bool _isInitialized;

        public void Show(PlayerPopupViewModel viewModel)
        {
            if (_viewModel != viewModel)
            {
                Cleanup(); // Отписка и удаление старых подписок

                _viewModel = viewModel;
                _playerLevelView.Init(_viewModel.PlayerLevelModel);

                if (_statListView == null)
                    _statListView = new StatListView(_viewModel.StatsListView, _viewModel.CharacterStatsHolder);

                _statListView.Initialize();
            }

            _statListView.Reload();
            _viewModel.UserInfoAdapter.Show();
            _playerLevelView.Show();
            _statListView.Show();
        }

        public void Hide()
        {
            _viewModel?.UserInfoAdapter.Hide();
            _playerLevelView.Hide();
            _statListView.Hide();
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        private void Cleanup()
        {
            _statListView?.Dispose();
            _viewModel?.Dispose();

            _statListView = null;
            _viewModel = null;
            _isInitialized = false;
        }
    }
}
