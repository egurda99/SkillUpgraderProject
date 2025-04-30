namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupSectionsFactory
    {
        private readonly CharacterStatsHolder _characterStatsHolder;
        private readonly StatsListHandlerFactory _statsListHandlerFactory;

        private readonly PlayerLevel _playerLevel;

        private readonly PlayerLevelView _playerLevelView;

        private readonly UserInfo _userInfo;
        private readonly UserInfoView _userInfoView;


        public PlayerPopupSectionsFactory(CharacterStatsHolder characterStatsHolder,
            StatsListHandlerFactory statsListHandlerFactory, PlayerLevel playerLevel, PlayerLevelView playerLevelView,
            UserInfo userInfo, UserInfoView userInfoView)
        {
            _characterStatsHolder = characterStatsHolder;
            _statsListHandlerFactory = statsListHandlerFactory;
            _playerLevel = playerLevel;
            _playerLevelView = playerLevelView;
            _userInfo = userInfo;
            _userInfoView = userInfoView;
        }

        public CharacterStatsSectionViewModel CreateCharacterStatsSection()
        {
            return new CharacterStatsSectionViewModel(_characterStatsHolder, _statsListHandlerFactory);
        }

        public PlayerLevelSectionViewModel CreatePlayerLevelSection()
        {
            return new PlayerLevelSectionViewModel(_playerLevel, _playerLevelView);
        }

        public UserInfoSectionViewModel CreateUserInfoSection()
        {
            return new UserInfoSectionViewModel(_userInfo, _userInfoView);
        }
    }
}