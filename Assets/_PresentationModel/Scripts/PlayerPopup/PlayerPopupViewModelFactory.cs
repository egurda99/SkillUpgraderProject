namespace Lessons.Architecture.PM
{
    // public sealed class PlayerPopupViewModelFactory
    // {
    //     private readonly PlayerPopupView _playerPopupView;
    //     private readonly PlayerLevel _playerLevel;
    //     private readonly UserInfo _userInfo;
    //     private readonly StatViewFactory _statViewFactory;
    //     private readonly StatAdapterFactory _statAdapterFactory;
    //     private readonly CharacterStatsHolder _characterStatsHolder;
    //
    //     public PlayerPopupViewModelFactory(
    //         PlayerPopupView playerPopupView,
    //         PlayerLevel playerLevel,
    //         UserInfo userInfo,
    //         StatViewFactory statViewFactory,
    //         StatAdapterFactory statAdapterFactory,
    //         CharacterStatsHolder characterStatsHolder)
    //     {
    //         _playerPopupView = playerPopupView;
    //         _playerLevel = playerLevel;
    //         _userInfo = userInfo;
    //         _statViewFactory = statViewFactory;
    //         _statAdapterFactory = statAdapterFactory;
    //         _characterStatsHolder = characterStatsHolder;
    //     }
    //
    //     public PlayerPopupViewModel Create()
    //     {
    //         return new PlayerPopupViewModel(
    //             _playerLevel,
    //             _userInfo,
    //             _playerPopupView.UserInfoView,
    //             _statViewFactory,
    //             _statAdapterFactory,
    //             _characterStatsHolder
    //         );
    //     }
    // }
}
