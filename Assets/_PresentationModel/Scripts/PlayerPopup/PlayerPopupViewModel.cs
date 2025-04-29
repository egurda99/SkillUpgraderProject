using System;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupViewModel : IDisposable
    {
        private readonly PlayerPresentationModel _playerLevelModel;
        private readonly UserInfoAdapter _userInfoAdapter;
        private readonly StatsListView _statsListView;
        private readonly CharacterStatsHolder _characterStatsHolder;

        public PlayerPresentationModel PlayerLevelModel => _playerLevelModel;

        public UserInfoAdapter UserInfoAdapter => _userInfoAdapter;

        public StatsListView StatsListView => _statsListView;

        public CharacterStatsHolder CharacterStatsHolder => _characterStatsHolder;


        public PlayerPopupViewModel(
            PlayerLevel playerLevel,
            UserInfo userInfo,
            UserInfoView userInfoView,
            StatViewFactory statViewFactory,
            StatAdapterFactory statAdapterFactory,
            CharacterStatsHolder characterStatsHolder)
        {
            _playerLevelModel = new PlayerPresentationModel(playerLevel);
            _userInfoAdapter = new UserInfoAdapter(userInfo, userInfoView);
            _statsListView = new StatsListView(statViewFactory, statAdapterFactory);
            _characterStatsHolder = characterStatsHolder;
        }

        public void Dispose()
        {
            _playerLevelModel.Dispose();
            _userInfoAdapter.Dispose();
        }
    }
}
