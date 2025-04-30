namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelSectionViewModel : ISectionViewModel
    {
        private readonly PlayerLevel _playerLevelModel;
        private readonly PlayerLevelView _playerLevelView;
        private readonly PlayerPresentationModel _playerPresentationModel;

        public PlayerLevelSectionViewModel(PlayerLevel playerLevelModel, PlayerLevelView playerLevelView)
        {
            _playerLevelModel = playerLevelModel;
            _playerLevelView = playerLevelView;
            _playerPresentationModel = new PlayerPresentationModel(_playerLevelModel);
            _playerLevelView.Init(_playerPresentationModel);
        }

        public void Show()
        {
            _playerLevelView.Show();
        }

        public void Hide()
        {
            _playerLevelView.Hide();
        }


        public void Dispose()
        {
            _playerPresentationModel.Dispose();
        }
    }
}
