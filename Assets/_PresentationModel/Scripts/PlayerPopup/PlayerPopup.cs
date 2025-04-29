using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopup : Popup
    {
        [SerializeField] private PlayerLevelView _playerLevelView;
        [SerializeField] private UserInfoView _userInfoView;

        private StatListView _statListView;
        private UserInfoAdapter _userInfoAdapter;
        private StatViewFactory _statViewFactory;
        private StatAdapterFactory _statAdapterFactory;
        private CharacterStatsHolder _characterStatsHolder;
        private PlayerPresentationModel _playerPresentaionModel;

        [Inject]
        public void Construct(StatViewFactory statViewFactory, StatAdapterFactory statAdapterFactory,
            CharacterStatsHolder characterStatsHolder,
            UserInfo userInfo, PlayerLevel playerLevel)
        {
            // level info part
            _playerPresentaionModel = new PlayerPresentationModel(playerLevel);
            _playerLevelView.Init(_playerPresentaionModel);

            // user info part
            _userInfoAdapter = new UserInfoAdapter(userInfo, _userInfoView);

            // Stats part
            _statViewFactory = statViewFactory;
            _statAdapterFactory = statAdapterFactory;
            _characterStatsHolder = characterStatsHolder;
            var statsListView = new StatsListView(_statViewFactory, _statAdapterFactory);
            _statListView = new StatListView(statsListView, _characterStatsHolder);
            _statListView.Initialize();
        }

        protected override void OnShow()
        {
            _playerLevelView.Show();
            _statListView.Show();
            _userInfoAdapter.Show();
        }

        protected override void OnHide()
        {
            _playerLevelView.Hide();
            _statListView.Hide();
            _userInfoAdapter.Hide();
        }

        private void OnDestroy()
        {
            _userInfoAdapter?.Dispose();
            _statListView?.Dispose();
            _playerPresentaionModel?.Dispose();
        }
    }
}
